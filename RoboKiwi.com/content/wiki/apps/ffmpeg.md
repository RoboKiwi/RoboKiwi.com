---
title: FFMpeg
description: FFMpeg tips and tricks
guid: "119aa225-ea06-4f5b-b672-52c6acde890c"
---

## Convert format

To do a basic, automatic conversion, just specify the input file, and set a different extension on the output and FFMpeg will usually do the rest:

`ffmpeg -i input.opus output.ogg`

## Trimming & Slicing

When exporting portions of a file, unless you're changing codec, you should use `copy` for the audio and video codecs.

* `-ss` to set the starting time: `hh:mm:ss.mm`
* `-to` to set the ending time: `hh:mm:ss.mm`
* `-t` to set the length of time in seconds to copy to the new stream.

e.g. to skip to 10 seconds in, and take the next 20 seconds and output to trimmed.mp4:

`ffmpeg -i source.mp4 -acodec copy -vcodec copy -ss 00:00:10 -t 10 trimmed.mp4`

To take a slice with a beginning time of 10 secs and an ending time of 30 secs into the video (for a total of 20 secs):

`ffmpeg -i source.mp4 -acodec copy -vcodec copy -ss 00:00:10 -to 00:00:30 trimmed.mp4`

## Stripping audio, video or subtitles

* `-an` No audio
* `-sn` No subtitles
* `-vn` No video
