ASP .Net core 2.2
Clean Architecture


https://github.com/ivanpaulovich/clean-architecture-manga

![Layers](https://docs.microsoft.com/en-us/dotnet/standard/modern-web-apps-azure-architecture/media/image5-7.png)

I need to point out that **Business Rules and Use Cases** should be implemented inside the Application Layer and they need to be maintained for the project’s life, in the other hand everything that give support for external capabilities are just **external details**, they can be replaced for different reasons, and we do not want the business rules to be coupled to them.

It is important to distinguish between the business and the details.

## :scissors: Business Rules and Use Cases
The business rules are the fine grained rules, they encapsulate entity fields and constraints. Also the business rules are the use cases that interacts with multiple entities and services. They together creates a process in the application, they should be sustained for a long time.

If the difference remain not clear, this Uncle Bob quote will clarify:

> Business Rules would make or save the business money, irrespective of whether they were implemented on a computer. They would make or save money even if they were executed manually.


## :fire: Layers
Let’s describe the Dependency Diagram below:

![Layers](https://raw.githubusercontent.com/ivanpaulovich/clean-architecture-manga/master/docs/Untitled-Diagram-1.png)

- The Domain Layer is totally independent of other layers and frameworks.
- The Application Layer depends exclusively on the Domain Layer.
- The Application Layer is independent of frameworks, databases and UI.
- The UI Layer and the Infrastructure Layer provides implementations for the Application needs.
- The UI Layer depends on Application Layer and it loads the Infrastructure Layer by indirection.

We should pay attention that the Infrastructure Layer can have many concerns. I recommend to design the infrastructure in a way you can split it when necessary, particularly when you have distinct adapters with overlapping concerns.

It is important to highlight the dashed arrow from the UI Layer to the Infrastructure layer. That is the where **Dependency Injection** is implemented, the concretions are loaded closer to the Main function. And there is a single setting in a external file that decides all the dependencies to be loaded.

## :star: Application Layer
Let’s dig into the Application Business Rules implemented by the Use Cases in our Bounded Context. As said by Uncle Bob in his book Clean Architecture:

> Just as the plans for a house or a library scream about the use cases of those buildings, so should the architecture of a software application scream about the use cases of the application.

Use Cases implementations are first-class modules in the root of this layer. The shape of a Use Case is an Interactor object that receives an Input, do some work then pass the Output through the caller. That’s the reason I am an advocate of feature folders describing the use cases and inside them the necessary classes:

![Use Cases](https://raw.githubusercontent.com/ivanpaulovich/clean-architecture-manga/master/docs/Use-Cases.png)

At your first look of the solution folders, you can build an idea of the purpose of this software. It seems like it can manage your Banck Account, for example you can Deposit or Withdraw money. Following we see the communication between the layers:

![Use Cases](https://raw.githubusercontent.com/ivanpaulovich/clean-architecture-manga/master/docs/Clean-Architecture-Style.png)

The Application exposes an interface (Port) to the UI Layer and another interface (another Port) to the Infrastructure Layer. What have you seen until here is Enterprise + Application Business Rules enforced without frameworks dependencies or without database coupling. Every details has abstractions protecting the Business Rules to be coupled to tech stuff.

In our implementation we have the following feature folders for every use case:

![Use Cases](https://raw.githubusercontent.com/ivanpaulovich/clean-architecture-manga/master/docs/Web-Use-Cases.png)

- **Request**: a data structure for the user input (accountId and amount).
- A **Controller** with an Action: this component receives the DepositRequest, calls the appropriate Deposit Use Case which do some processing then pass the output through the Presenter instance.
- **Presenter**: it converters the Output to the Model.
- **Model**: this is the return data structure for MVC applications.

We must highlight that the Controller knows the Deposit Use Case and it is not interested about the Output, instead the Controller delegates the responsibility of generating a Model to the Presenter instance.

```csharp
[Route("api/[controller]")]
public class AccountsController : Controller
{
    private readonly IDepositUseCase _depositUseCase;
    private readonly Presenter _presenter;

    public AccountsController(
        IDepositUseCase depositUseCase,
        Presenter presenter)
    {
        _depositUseCase = depositUseCase;
        _presenter = presenter;
    }

    /// <summary>
    /// Deposit to an account
    /// </summary>
    [HttpPatch("Deposit")]
    public async Task<IActionResult> Deposit([FromBody]DepositRequest message)
    {
        var output = await _depositUseCase.Execute(message.AccountId, message.Amount);
        _presenter.Populate(output);
        return _presenter.ViewModel;
    }
}
```

An Presenter class is detailed bellow and it shows a conversion from the DepositOutput to two different ViewModels. One ViewModel for null Outputs and another ViewModel for successful deposits.

```csharp
public class Presenter
{
    public IActionResult ViewModel { get; private set; }

    public void Populate(DepositOutput output)
    {
        if (output == null)
        {
            ViewModel = new NoContentResult();
            return;
        }

        ViewModel = new ObjectResult(new CurrentAccountBalanceModel(
            output.Transaction.Amount,
            output.Transaction.Description,
            output.Transaction.TransactionDate,
            output.UpdatedBalance
        ));
    }
}
```