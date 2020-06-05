using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace Plugin
{
    [ApiVersion(2, 1)]
    public class Plugin : TerrariaPlugin
    {
        const string BaseGroupName = "chocolabase";
        const string GroupName = "chocola";

        public override string Author => "Pryaxis, modded by SGKoishi";

        public override string Description => "Configure your server for vanilla gameplay";

        public override string Name => "Vanillafier";

        public override Version Version => Assembly.GetExecutingAssembly().GetName().Version;

        public Plugin(Main game) : base(game)
        {
        }

        public override void Initialize()
        {
            var basePermissions = string.Join(",",
                "tshock.account.*",
                "tshock.npc.hurttown",
                "tshock.npc.startinvasion",
                "tshock.npc.startdd2",
                "tshock.npc.summonboss",
                "tshock.npc.spawnpets",
                "tshock.tp.others",
                "tshock.tp.rod",
                "tshock.tp.wormhole",
                "tshock.tp.pylon",
                "tshock.world.editspawn",
                "tshock.world.modify",
                "tshock.world.movenpc",
                "tshock.world.paint",
                "tshock.world.time.usesundial",
                "tshock.world.toggleparty",
                "tshock.canchat",
                "tshock.sendemoji",
                "tshock.partychat",
                "tshock.thirdperson",
                "tshock.whisper",
                "tshock.journey.*",
                "tshock.synclocalarea"
            );

            if (!TShock.Groups.GroupExists(BaseGroupName))
            {
                TShock.Groups.AddGroup(name: BaseGroupName, parentname: null, permissions: basePermissions, chatcolor: Group.defaultChatColor);
            }
            else
            {
                TShock.Groups.UpdateGroup(name: GroupName, parentname: null, permissions: basePermissions, chatcolor: Group.defaultChatColor, suffix: null, prefix: null);
            }

            if (!TShock.Groups.GroupExists(GroupName))
            {
                TShock.Groups.AddGroup(name: GroupName, parentname: BaseGroupName, permissions: null, chatcolor: Group.defaultChatColor);
            }
            else
            {
                var existing = TShock.Groups.GetGroupByName(GroupName);
                TShock.Groups.UpdateGroup(name: GroupName, parentname: BaseGroupName, permissions: existing.Permissions, chatcolor: existing.ChatColor, suffix: existing.Suffix, prefix: existing.Prefix);
            }

            TShock.Config.DefaultRegistrationGroupName = GroupName;
            TShock.Config.Write(typeof(FileTools).GetProperty("ConfigPath", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null) as string);

            Commands.ChatCommands.Add(new Command(Permissions.managegroup, this.Vanillafy, "vanillafy"));
        }

        private void Vanillafy(CommandArgs args)
        {
            TShock.UserAccounts.GetUserAccounts()
                .Where(u => !TShock.Groups.GetGroupByName(u.Group).HasPermission(Permissions.managegroup))
                .ForEach(u => TShock.UserAccounts.SetUserGroup(u, GroupName));
            args.Player.SendSuccessMessage("已将所有玩家移动至原版玩家组内。");
        }
    }
}
