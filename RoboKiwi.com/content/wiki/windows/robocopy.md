---
title: RoboCopy
guid: "98866d7e-3f05-42b6-ad7f-0845e8c4faeb"
---

# Backup / Mirror Drive

```
RoboCopy E: F: /e /MT:8 /r:5 /w:10 /log:robocopy.log /tee
```

Copies all files and sub directories, including empty directories

Uses 8 threads

Retries files that can't be accessed up to 5 times, waiting for 10 seconds between each retry

Writes to robocopy.log, and also to the console.

```
RoboCopy E: F: /e /MT:16 /r:5 /w:10 /log:robocopy.log /tee /e /purge
```

Additionally, removes directories and files from the destination if they don't exist in the source.

## References

https://docs.microsoft.com/windows-server/administration/windows-commands/robocopy
