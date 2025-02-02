# SimpleAudio

A pooled audio manager that can be accessed with a singleton, to avoid having to place AudioSources in every GameObject. It supports playing a 2D sound by using the components AudioManager, but also allows 3D spatial sound by instantiating AudioManagers with spatial blend in different positions. The spatial audiomanagers are saved in an object pool, so they can be reused to avoid having to instantiate the objects every time. 

## Contents
- **SimpleAudio Monobehaviour**: This behaviour is attached to the SimpleAudio prefab, and should be unique (since it's accessed as a singleton). It allows playing sounds by providing an AudioClip and optionally a position if spatial blend is required.
- **AudioBag Monobehaviour**: This behaviour can be attached to any GameObject, and will act as a dictionary for AudioClips. You can provide a label and a list of AudioClips, and when calling the *PlayAny* method, a random sound of the corresponding list will play using the SimpleAudio component.
- **SimpleAudio prefab**: This prefab need to be placed on the scene, and it will instantiate the required components in order for the audio manager to work.

## How to use
1. Place the SimpleAudio prefab inside your Unity scene
2. You can now:
   1. Call the *SimpleAudio.Play* method from any Monobehaviour to play the provided AudioClip.
   2. Call the *SimpleAudio.PlayAtPoint* method from any Monobehaviour to play the provided AudioClip at certain position (spatial sound)
   3. Place *AudioBag* components in the different GameObjects that emit sound, in order to store AudioClips by label, instead of having to add AudioClip fields for every sound that you want to play. The AudioBag component has a *PlayAny* and a *PlayAnyAtPoint* methods that will call the SimpleAudio component by randomly selecting a sound from the list corresponding to the label passed as parameter.

Check the documentation inside the corresponding classes to learn about the different parameters you can send to the methods described above.
