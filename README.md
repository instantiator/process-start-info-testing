# ProcessStartInfoTesting

Documentation for [UseShellExecute](https://docs.microsoft.com/en-us/dotnet/api/system.diagnostics.processstartinfo.useshellexecute?view=net-5.0)
states that:

> When `UseShellExecute` is `false`, the `FileName` property can be either a fully qualified path to the
> executable, or a simple executable name that the system will attempt to find within folders specified
> by the `PATH` environment variable.

Some quick experiments with `ProcessStartInfo` with `UseShellExecute == false` give conflicting results on Mac OS, Big Sur:

* Some binaries, like `docker`, can be reliably found and executed.
* Others, like `ffmpeg`, cannot _when launched from Visual Studio._
* When the tests are launched using command line `dotnet` tool, they can be found and executed, too.

## Results from Visual Studio

![Results showing that docker can be found on the PATH, but ffmpeg cannot.](images/test-run-results.png)

Test `CanFind_ffmpeg_WithoutShellExecute` fails by throwing this `Win32Exception`:

> Test method `ProcessStartInfoTesting.UseShellExecuteTests.CanFind_ffmpeg_WithoutShellExecute` threw exception: `System.ComponentModel.Win32Exception: No such file or directory`

## Results from the CLI

Running the tests using the donet CLI succeeds:

```bash
dotnet test
Passed!  - Failed:     0, Passed:     5, Skipped:     0, Total:     5, Duration: 882 ms
```

## Specifics

On my system, both `docker` and `ffmpeg` are provided on the `PATH` as links:

```
$ which ffmpeg
/opt/homebrew/bin/ffmpeg

$ ls -lh /opt/homebrew/bin/ffmpeg
lrwxr-xr-x  1 lewiswestbury  admin    35B Nov  1 13:49 /opt/homebrew/bin/ffmpeg -> ../Cellar/ffmpeg/4.4.1_2/bin/ffmpeg

$ which docker                   
/usr/local/bin/docker

$ ls -lh /usr/local/bin/docker
lrwxr-xr-x  1 lewiswestbury  wheel    54B Mar  9  2021 /usr/local/bin/docker -> /Applications/Docker.app/Contents/Resources/bin/docker
```

My path definitely contains entries for `/opt/homebrew/bin` and `/usr/local/bin`.

## Prerequisites

Install `docker` and `ffmpeg` with [Homebrew](https://brew.sh/), as here:

```bash
brew install ffmpeg
brew install --cask docker
```
