# Credits
Copyright (c) Martônio Júnior 2022, uses the MIT License

# About

Trinkets is a custom Unity Package which proposes a solution for traditional game resources that allows for easy implementation in any kind of game. Inspired by things like Apple's GameplayKit, Machinations and ScriptableObject Architecture, the goal of this package is to offer an architecture that is made so that:

* Designers can easily add basic resources into a game, as well as simple logic to create more common interactions.
* Programmers have the tools to extend and create their own custom resources and components with ease.
* Both types of users can work together to create games better and faster.

# How to Install

Via Unity Package Manager:
1. In Unity, go to "Window" -> "Package Manager"
2. Click the + button, and select "Add package from git URL..."
3. Enter the URL of this repository:
    * https://github.com/MartonioJunior/Trinkets.git

# Package Features
The descriptions below are a simple overview of what the package has, so you can know what you get by installing Trinkets. For a more detailed breakdown about the features, as well how to use the package, please check the documentation (coming soon).

## Types of Resources Available
Trinkets offers 3 types of resources for you right off the bat:

* Currencies: Resources that are exclusively measured by a numerical quantity.
* Collectables: Instances that are considered unique inside the game.
* Items: Resources instanced by model that can contain unique instance data, as well as general information.

## Architecture
The architecture of the package centers around 2 main elements:
* **Resources**, spread throughout the game.
* **Wallets**, which are resource storages.

While the types of resources available do have different ways to handle the architecture, there's a set of tools that's constant throughout the package:

* **Categories**, which allow for better grouping and categorization of resources. Available for Collectables.
* **Instancers**, components that insert resources into wallets. Available for all types.
* **Scanners**, which check if the wallet has hit a certain threshold of resources. Available for all types.
* **EventListeners**, components that allow to check for certain elements in a wallet. Available for all types.

With the set of tools above, we're able to create simple, yet really effective game design contraptions, such as:
* Doors that only open once you have enough of a resource.
* Simple trade systems that allow for exchanging, event between different resource types.
* Simple progression gates once you have enough on a category.
* Revealing/hiding objects if you have a certain item.

and many other use cases...

## Interface-based Approach

If you're looking to create a new resource type, a new variant or your own custom component, there are interfaces for creating all of the component types above, as well as specific interfaces for each type of resource.

For more details, please check the documentation.

# Considerations
This project is still in a experimental phase, where I'm discovering what works and what doesn't before moving into the next steps. As such, some aspects of the API may change over time. That said, feel free to drop an issue or feedback about the project in the Issues Tab.