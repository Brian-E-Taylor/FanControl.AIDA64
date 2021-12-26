# FanControl.AIDA64

Plugin for [FanControl](https://github.com/Rem0o/FanControl.Releases) that provides support for AIDA64 sensors using its shared memory feature.

## To install

Either
* Download the latest binaries from [Releases]()
* Compile the solution.

And then

1. Copy the bin/release content into FanControl's "plugins" folder.
2. Make sure AIDA64 has WMI turned on for External Applications (File -> Preferences, Hardware Monitoring -> External Applications, make sure "`Enable writing sensor values to WMI`" is checked).