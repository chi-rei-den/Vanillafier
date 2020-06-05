# Vanillafier
Vanillafier provides a quick way to setup vanilla server, adapted from [Vanillafier](https://github.com/Pryaxis/Vanillafier/)

## What it does:
* Creates new group `chocolabase` with basic permissions
* Creates new group `chocola`, inherited from `chocolabase`
* Updates your TShock config file so that new users & guest users will be added to `chocola` group
* Move all players to `chocola` group (`/vanillafy`, requires `tshock.admin.group` permission)

The plugin does not include a way to reverse this process.

## How to use it:
1. Install the plugin
2. If any registered users exist, run the `/vanillafy` command
3. Complete
