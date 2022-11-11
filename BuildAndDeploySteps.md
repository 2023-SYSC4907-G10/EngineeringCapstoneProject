# BUILDING

## Build Hosting Process

- Deployment will be based on the "Production" branch
- PR as normal into "main" branch
  - Do not include new builds into PRs into main. Code changes only
  - Updating builds will be group approved PRs into "main"
  - "main" will eventually get PR'd into "Production"
- Deployment URL : <https://2023-sysc4907-g10.github.io/EngineeringCapstoneProject/>

## Building in Unity

- File>Build Settings>WebGL
- Player options (Bottom left corner of build settings in WebGL)
  - If there's a warning for the colors in build settings
    - OtherSettings>ColorSpace // set to Gamma
  - Make sure that "Decompression Fallback" is enabled in Publishing settings
- Back in build settings, click "Switch Platform" in WebGL if build is not clickable
- Hit build.
- Mandatory to put this build into a folder named "builds". Do this outside the repo
- Take the html file and 2 folders made from the build and put them in the top level of the repo
- Push and then PR

## Build Notes

- Deployment to Windows as .exe is similarly easy to dev, but worse for the end user to access
  - Out of browser would have better performance
  - May need to revisit the web only delivery if performance or download size becomes an issue
