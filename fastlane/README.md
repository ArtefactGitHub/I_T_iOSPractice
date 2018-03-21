fastlane documentation
================
# Installation

Make sure you have the latest version of the Xcode command line tools installed:

```
xcode-select --install
```

Install _fastlane_ using
```
[sudo] gem install fastlane -NV
```
or alternatively using `brew cask install fastlane`

# Available Actions
## iOS
### ios version
```
fastlane ios version
```
Sets the target application version or displays the current one
### ios provision
```
fastlane ios provision
```
Install provisioning profiles using match
### ios beta
```
fastlane ios beta
```
Deploys the app to testflight
### ios store
```
fastlane ios store
```
Deploys the app to the appstore
### ios release_branch
```
fastlane ios release_branch
```


----

This README.md is auto-generated and will be re-generated every time [fastlane](https://fastlane.tools) is run.
More information about fastlane can be found on [fastlane.tools](https://fastlane.tools).
The documentation of fastlane can be found on [docs.fastlane.tools](https://docs.fastlane.tools).
