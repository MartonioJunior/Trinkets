# Changelog

Based on the [Keep a Changelog](https://keepachangelog.com/en/1.0.0/) format.

## [Unreleased]

## [0.0.3] - 2022-08-12
### Added
- Wallets now show their contents in the inspector
### Changed
- CollectableData now allows for a custom icon and display name
    - If an icon was not selected, Image uses the attached category's icon instead.
    - If a display name was not set, Name returns the object's name and the category it belongs to

## [0.0.2] - 2022-08-10
### Added
- Basic documentation for the package

### Changed
- Interface Inspector Fields are labelled as experimental and locked via ENABLE_INSPECTOR_FIELDS preprocessor due to constant errors.

### Fixed
- CollectableScannerCategoryComponent: PerformTax now correctly returns false when the amount to remove from a category is zero. 
- CollectableScannerComponent: PerformTax now returns false when there's no collectables in the List. 

### Removed
- Unused methods on the EngineBehaviour, CollectionExtensions and EngineScrob classes.

### Github Repository
- Added Github Workflows for Continuous Integration

## [0.0.1] - 2022-08-01
### Added
- New module: Currencies
    - New object: Currency Data
    - New object: Currency Wallet
    - New component: Currency Giver
    - New component: Currency Scanner
    - New component: Currency Event Listener
- New module: Collectables
    - New object: Collectable Data
    - New object: Collectable Wallet
    - New component: Collectable Giver
    - New component: Collectable Scanner
    - New component: Collectable Category Scanner
    - New component: Collectable Event Listener

### Changed
Nothing on this release

### Deprecated
Nothing on this release

### Removed
Nothing on this release

### Fixed
Nothing on this release
