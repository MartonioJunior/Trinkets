# Credits
Copyright (c) Martônio Júnior 2022, uses the MIT License

# About

Trinkets is a custom Unity Package which proposes a solution for traditional game resources that allows for easy implementation in any kind of game. Inspired by things like Apple's GameplayKit, Machinations and ScriptableObject Architecture, the goal of this package is to offer an architecture that is made so that:

* Designers can easily add basic resources into a game, as well as simple logic to create more common interactions.
* Programmers have the tools to extend and create their own custom resources and components with ease.
* Both types of users can work together to create games better and faster.

This package is available and tested for Unity 2021 LTS onwards.

# How to Install

Via Unity Package Manager:
1. In Unity, go to "Window" -> "Package Manager"
2. Click the + button, and select "Add package from git URL..."
3. Enter the URL of this repository:
    * https://github.com/MartonioJunior/Trinkets.git

## When should you consider using Trinkets?

Trinkets was made as a KISS (Keep it Simple, Student) solution to structure game resources on my personal projects from a game designer's perspective that was also flexible enough that I could extend and add new features as I see fit. As such, my recommendation is to use Trinkets is when you want a out-of-the-box solution for adding in-game currencies and collectables to your game.

# Package Features
The descriptions below are a simple overview of what the package has, so you can know what you get by installing Trinkets. For a more detailed breakdown about the features, as well how to use the package, please check the documentation (coming soon).

## Types of Resources Available
Trinkets offers 2 types of resources:

* **Currencies**: Resources that are exclusively measured by a numerical quantity.
* **Collectables**: Instances that are considered unique inside the game.

## Architecture
The architecture of the package centers around 2 main elements:
* **Resources**, the basic unit used to describe in-game resources.
* **Wallets**, a single resource storage for in-game resources.

While the types of resources available do have different implementations, they all are based on the same common implementation for resources and wallets. With that, any component created can be used by currencies and collectables alike.

By default, Trinkets implements the following:

* **Instancers**, components responsible for inserting resources into wallets.
* **Drainers**, components responsible for removing resources from wallets.
* **Scanners**, which check if the wallet has hit a certain threshold of resources.
* **Pockets**, responsible for giving a physical presence to wallets.
* **Detectors**, triggers that invoke events when a wallet is detected.
* **Wallet Listeners**, components that allow to check for certain elements in a wallet.

With the set of tools above, we're able to create simple, yet really effective game design contraptions, such as:
* Doors that only open once you have enough of a resource.
* Simple trade systems that allow for exchanging, event between different resource types.
* Simple progression gates once you have enough on a category.
* Revealing/hiding objects if you have a certain item.

and many other use cases...

## Quick Start

To start using Trinkets in your project, follow these X steps:
1. Right-click in the project page and select "Create -> Trinkets" to create any of the following objects:
- Currency
- Collectable
- Collectable Category (classifies Collectables)
- Currency Wallet (stores Currencies and nothing else)
- Collectable Wallet (stores Collectables and nothing else)

2. On any GameObject, select "Add Component -> Trinkets" and select any of the  components available:
- Pockets to make Wallets interact physically with other elements
- A detector combined with an Instancer, Drainer or Scanner to perform operations in any detected wallet
- Wallet Listener to make any element react to amount changes of a resource inside a wallet

3. And there ya go, it's already part of your project.

# Considerations
This project is in a preview phase, where I'm discovering what works and what doesn't before moving into the next steps, making breaking changes to the API along the way until the scope is perfected to 1.0.0. The project is already usable in the current state, but please note that upgrading into a newer version is likely to have breaking changes. Feel free to drop an issue or feedback about the project in the Issues Tab.