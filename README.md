# Credits

Copyright (c) Martônio Júnior 2022, uses the MIT License

# About

Trinkets is a custom Unity Package which proposes a solution for traditional game resources that allows for easy implementation in any kind of game. Inspired by things like Apple's GameplayKit, Machinations and ScriptableObject Architecture, the goal of this package is to offer an architecture that is made so that:

- Designers can easily add basic resources into a game, as well as simple logic to create more common interactions.
- Programmers have the tools to extend and create their own custom resources and components with ease.
- Both types of users can work together to create games better and faster.

Currently the package supports Unity 2019 LTS onwards during it's preview phase.

# How to Install

Via Unity Package Manager:

1. In Unity, go to "Window" -> "Package Manager"
2. Click the + button, and select "Add package from git URL..."
3. Enter the URL of this repository:
   - https://github.com/MartonioJunior/Trinkets.git

# Package Features

The descriptions below are a simple overview of what the package has, so you can know what you get by installing Trinkets. For a more detailed breakdown about the features, as well how to use the package, please check the documentation (coming soon).

## Types of Resources Available

Trinkets offers 2 types of resources for you right off the bat:

- **Currencies**: Resources that are exclusively measured by a numerical quantity.
- **Collectables**: Instances that are considered unique inside the game.

## Architecture

The architecture of the package centers around 2 main elements:

- **Resources**, spread throughout the game.
- **Wallets**, which are resource storages.

While the types of resources available do have different ways to handle the architecture, there's a set of tools that's constant throughout the package:

- **Categories**, which allow for better grouping and categorization of resources. Available for Collectables.
- **Instancers**, components that insert resources into wallets. Available for all types.
- **Scanners**, which check if the wallet has hit a certain threshold of resources. Available for all types.
- **EventListeners**, components that allow to check for certain elements in a wallet. Available for all types.

With the set of tools above, we're able to create simple, yet really effective game design contraptions, such as:

- Doors that only open once you have enough of a resource.
- Simple trade systems that allow for exchanging, event between different resource types.
- Simple progression gates once you have enough on a category.
- Revealing/hiding objects if you have a certain item.

and many other use cases...

## Quick Start

To start using Trinkets in your project, follow these X steps:

1. Right-click in the project page and select "Create -> Trinkets" to create any of the following objects:

- Currency
- Collectable
- Collectable Category (classifies Collectables)
- Currency Wallet (stores Currencies)
- Collectable Wallet (stores Collectables)

2. On any GameObject, select "Add Component -> Trinkets" and select any of the following components:

- Currency Giver (adds Currency to a Currency Wallet)
- Currency Scanner (checks and/or taxes a Currency Wallet)
- Currency Event Listener (invokes an event for the amount of Currency in a Currency Wallet)
- Collectable Giver (adds a Collectable to a Wallet)
- Collectable Scanner (checks and/or taxes a Collectable Wallet by the specified Collectables)
- Collectable Category Scanner (checks and/or taxes a Collectable Wallet by the Collectable Category)
- Collectable Event Listener (invokes an event for whether there's a Collectable inside a Currency Wallet)

3. Inside the code or using Unity Events, call the methods of the component and pass the wallet as a parameter.

And there ya go, it's already part of your project.

# Considerations

This project is still in a experimental phase, where I'm discovering what works and what doesn't before moving into the next steps. As such, some aspects of the API may change over time. That said, feel free to drop an issue or feedback about the project in the Issues Tab.
