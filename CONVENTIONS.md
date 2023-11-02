# C# Code Conventie

Dit document beschrijft de codeconventie voor het C#-project Barroc. Intens

## Bestandsnaamconventie

- Bestandsnamen moeten in PascalCase zijn en de naam van de klasse of het hoofdbestand weerspiegelen. Bijvoorbeeld: `MyClass.cs`, `Program.cs`.

## Indentatie en Inspringing

- Gebruik  **tabs** om in te springen.
- Vermijd het gebruik van vier spaties voor inspringing.

## Stijlrichtlijnen

- Gebruik **PascalCase** voor klasse- en type-namen. Bijvoorbeeld: `MyClass`, `PersonModel`.
- Gebruik **camelCase** voor variabelen en parameters. Bijvoorbeeld: `myVariable`, `myParameter`.
- Gebruik **UpperCamelCase** voor naamgeving van methoden. Bijvoorbeeld: `CalculateTotal()`, `GetCustomerDetails()`.
- Gebruik **lowerCamelCase** voor fields Bijvoorbeeld:  private string currentLocation;
## Opmaak

- Beperk regellengte tot **maximaal 100 tekens**.
- Voeg **lege regels** toe tussen logische secties van de code om de leesbaarheid te verbeteren.
- Gebruik accolades op een nieuwe regel voor methoden, klassen, enz.
- DRY Don't repeat yourself.

```csharp
public class MyClass
{
    public void MyMethod()
    {
        // Code hier
    }
}


