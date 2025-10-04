---
title: KeePassXC
---

## Overview

KeePassXC is a free, Open Source cross-platform password manager compatible with KeePass databases.

## SSH setup

You can integrate KeePassXC with the OpenSSH agent, to manage your SSH keys in KeePassXC.

### OpenSSH Setup

Remove existing Windows feature:

```powershell
# From an administrative Powershell terminal
Remove-WindowsCapability -Online -Name "OpenSSH.Client~~~~0.0.1.0"
```

Otherwise `Settings` > `Apps` > `Optional Features`

Install / upgrade:

`winget install --id Microsoft.OpenSSH.Beta`

You'll likely need to restart, and it will be added to the system path (`C:\Program Files\OpenSSH\`)

You can also remove the Windows feature:

And check to remove from your PATH:

`C:\WINDOWS\System32\OpenSSH\` and `%SYSTEMROOT%\System32\OpenSSH\`

Also stop and set the OpenSSH Server to Disable after the restart

Verify that Windows is using the correct ssh-agent

`> where.exe ssh-agent`

Should give you:

`C:\Windows\System32\OpenSSH\ssh-agent.exe`

```cmd
> ssh -V
OpenSSH_for_Windows_8.6p1, LibreSSL 3.4.3

> ssh-add -l
The agent has no identities.
```

Generate key:

`ssh-keygen -t ed25519`

C:\Users\username/.ssh/id_ed25519.
C:\Users\username/.ssh/id_ed25519.pub

Ensure the SSH Agent is set to start up automatically, and start it now.

```powershell
Get-Service ssh-agent | Set-Service -StartupType Automatic
Start-Service ssh-agent
Get-Service ssh-agent
```

### Configure KeePassXC

In KeePassXC, configure your keys, and tick to add to the agent.

Make sure the SSH agent is enabled in Options; tick to use OpenSSH (if you try Both, it will often say it's not running)
Lock the database and unlock again to force it to hydrate the SSH Agent file

Now you should see your keys:

```cmd
> ssh-add -l
256 SHA256:<key> jane@doe.com (ED25519)
```

### GitHub SSH Setup

Test you can auth to GitHub:

```cmd
> ssh -T git@github.com
Hi JaneDoe! You've successfully authenticated, but GitHub does not provide shell access.
```

Now load your key files into ssh-agent:

`ssh-add $env:USERPROFILE\.ssh\id_ed25519`

Trust the GitHub hosts by adding to `~/.ssh/known_hosts`:

```ssh
github.com ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIOMqqnkVzrm0SdG6UOoqKLsabgH5C9okWi0dh2l9GKJl
github.com ecdsa-sha2-nistp256 AAAAE2VjZHNhLXNoYTItbmlzdHAyNTYAAAAIbmlzdHAyNTYAAABBBEmKSENjQEezOmxkZMy7opKgwFB9nkt5YRrYMjNuG5N87uRgg6CLrbo5wAdT/y6v0mKV0U2w0WZ2YB/++Tpockg=
github.com ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQCj7ndNxQowgcQnjshcLrqPEiiphnt+VTTvDP6mHBL9j1aNUkY4Ue1gvwnGLVlOhGeYrnZaMgRK6+PKCUXaDbC7qtbW8gIkhL7aGCsOr/C56SJMy/BCZfxd1nWzAOxSDPgVsmerOBYfNqltV9/hWCqBywINIR+5dIg6JTJ72pcEpEjcYgXkE2YEFXV1JHnsKgbLWNlhScqb2UmyRkQyytRLtL+38TGxkxCflmO+5Z8CSSNY7GidjMIZ7Q4zMjA2n1nGrlTDkzwDCsw+wqFPGQA179cnfGWOWRVruj16z6XyvxvjJwbz0wQZ75XK5tKSb7FNyeIEs4TT4jk+S4dhPeAUC5y+bDYirYgM4GC7uEnztnZyaVWQ7B381AK4Qdrwt51ZqExKbQpTUNn+EjqoTwvqNj4kqx5QUCI0ThS/YkOxJCXmPUWZbhjpCg56i+2aB6CmK2JGhn57K5mj0MNdBXA4/WnwH6XoPWJzK5Nyu2zB3nAZp+S5hpQs+p1vN1/wsjk=
```

You can get the GitHub host SSH keys from the official GitHub API:

`gh api -H "Accept: application/vnd.github+json" --jq '.ssh_keys' /meta`

```json
[
  "ssh-ed25519 AAAAC3NzaC1lZDI1NTE5AAAAIOMqqnkVzrm0SdG6UOoqKLsabgH5C9okWi0dh2l9GKJl",
  "ecdsa-sha2-nistp256 AAAAE2VjZHNhLXNoYTItbmlzdHAyNTYAAAAIbmlzdHAyNTYAAABBBEmKSENjQEezOmxkZMy7opKgwFB9nkt5YRrYMjNuG5N87uRgg6CLrbo5wAdT/y6v0mKV0U2w0WZ2YB/++Tpockg=",
  "ssh-rsa AAAAB3NzaC1yc2EAAAADAQABAAABgQCj7ndNxQowgcQnjshcLrqPEiiphnt+VTTvDP6mHBL9j1aNUkY4Ue1gvwnGLVlOhGeYrnZaMgRK6+PKCUXaDbC7qtbW8gIkhL7aGCsOr/C56SJMy/BCZfxd1nWzAOxSDPgVsmerOBYfNqltV9/hWCqBywINIR+5dIg6JTJ72pcEpEjcYgXkE2YEFXV1JHnsKgbLWNlhScqb2UmyRkQyytRLtL+38TGxkxCflmO+5Z8CSSNY7GidjMIZ7Q4zMjA2n1nGrlTDkzwDCsw+wqFPGQA179cnfGWOWRVruj16z6XyvxvjJwbz0wQZ75XK5tKSb7FNyeIEs4TT4jk+S4dhPeAUC5y+bDYirYgM4GC7uEnztnZyaVWQ7B381AK4Qdrwt51ZqExKbQpTUNn+EjqoTwvqNj4kqx5QUCI0ThS/YkOxJCXmPUWZbhjpCg56i+2aB6CmK2JGhn57K5mj0MNdBXA4/WnwH6XoPWJzK5Nyu2zB3nAZp+S5hpQs+p1vN1/wsjk="
]
```

You can also get the fingerprints:

`gh api -H "Accept: application/vnd.github+json" --jq '.ssh_key_fingerprints' /meta`

```json
{
  "SHA256_ECDSA": "p2QAMXNIC1TJYWeIOttrVc98/R1BUFWu3/LiyKgUfQM",
  "SHA256_ED25519": "+DiY3wvvV6TuJJhbpZisF/zLDA0zPMSvHdkr4UvCOqU",
  "SHA256_RSA": "uNiVztksCsDhcc0u9e8BujQXVUpKZIDTMczCvj3tD2s"
}
```
