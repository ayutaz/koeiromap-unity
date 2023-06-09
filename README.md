# Koeiromap Unity

Library for Unity to use Koeiromap

[日本語ドキュメント(Japanese Documents Available)](README_JP.md).

Since it is very difficult to find the ideal voice by changing each parameter in Koeiromap, we are developing the following tool "KoeiromapIndex" to support finding the ideal voice.

[koeiromap-index](https://github.com/ayutaz/koeiromap-index)

## Demo

You can change the parameters and play any voice on the following screen from the [demo page](https://ayutaz.github.io/koeiromap-unity/WebGL/).

![](Docs/demo_en.jpg)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Table of Contents**

- [Installation](#installation)
  - [UPM](#upm)
  - [Unity Package](#unity-package)
- [requirements](#requirements)
- [how to use](#how-to-use)
  - [Sample Code](#sample-code)
- [3rd Party Notices](#3rd-party-notices)
- [License](#license)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Installation
## UPM
1. Open the Package Manager
2. Click the `+` button in the top left corner
3. Select `Add package from git URL...`
4. Add URL for `https://github.com/ayutaz/koeiromap-unity.git?path=Assets/KoeiromapUnity/Scripts`
5. Click `Add`

## Unity Package
1. Download the latest release from the [releases page](https://github.com/ayutaz/koeiromap-unity/releases)
2. Import the package into your project

# requirements
* Unity 2021.3.x or later
* [UniTask](https://github.com/Cysharp/UniTask)

# how to use

## Sample Code

``` csharp

var voiceParam = new VoiceParam
{
    text = "Hello",
    speaker_x = 2.0f,
    speaker_y = 2.0f,
    style = "talk",
    seed = "1234567890",
};
var voice = await Koeiromap.GetVoice(voiceParam, _cancellationTokenSource.Token);
_audioSource.clip = voice.audioClip;
_audioStringData = voice.audioBase64;
_audioSource.Play();

```

# 3rd Party Notices

See [NOTICE](NOTICE.md).

# License

[MIT License](https://github.com/ayutaz/koeiromap-unity/LICENSE)

[Font License](https://github.com/coz-m/MPLUS_FONTS/blob/master/OFL.txt)