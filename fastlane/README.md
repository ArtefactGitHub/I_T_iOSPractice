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
### ios Dev_Debug
```
fastlane ios Dev_Debug
```

### ios Dev_Release
```
fastlane ios Dev_Release
```

### ios AdHoc_Debug
```
fastlane ios AdHoc_Debug
```

### ios AdHoc_Release
```
fastlane ios AdHoc_Release
```

### ios Enterprise_Debug
```
fastlane ios Enterprise_Debug
```

### ios Enterprise_Release
```
fastlane ios Enterprise_Release
```

### ios AppStore
```
fastlane ios AppStore
```
Deploys the app to the AppStore
### ios test
```
fastlane ios test
```
Deploys the app to testflight
### ios provision
```
fastlane ios provision
```
Install provisioning profiles using match
### ios Increment
```
fastlane ios Increment
```
Increment CFBundleVersion of Info.plist
### ios IncrementalBuildNumber
```
fastlane ios IncrementalBuildNumber
```
Increment CFBundleVersion of Info.plist
### ios CleanAll
```
fastlane ios CleanAll
```
Clean All
### ios BuildLib
```
fastlane ios BuildLib
```
Build Static Library
### ios version
```
fastlane ios version
```
Sets the target application version or displays the current one

----

This README.md is auto-generated and will be re-generated every time [fastlane](https://fastlane.tools) is run.
More information about fastlane can be found on [fastlane.tools](https://fastlane.tools).
The documentation of fastlane can be found on [docs.fastlane.tools](https://docs.fastlane.tools).
