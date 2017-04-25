NAudio.VBAN 
-------

To use:

```cs
// add a reference to NAudio.dll

using (var vbanSender = new VBANSender(sourceIStreamProvider, "dest hostname", port, "Stream Name"))
using (var waveOut = new NAudio.Wave.WaveOutEvent())
{
    waveOut.Init(vbanSender);
    waveOut.Play();
}
```
