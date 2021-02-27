using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNDOneshotDecider
{
    public partial class Form1 : Form
    {
        public enum Quality
        {
            Worst = 0,
            Terrible = 1,
            Bad = 2,
            OK = 3,
            Good = 4,
            Best = 5
        }

        /// <summary>
        /// A special rule applied to the oneshot
        /// </summary>
        /// <remarks>
        /// Has a Name, Decription and Quality
        /// </remarks>
        public class Rule
        {
            public string _name, _desc;
            public Quality _quality;

            /// <summary>
            /// Initialises a new instance of the Rule class using the given parameters
            /// </summary>
            /// <param name="name">Name of the Rule</param>
            /// <param name="desc">Description of the Rule</param>
            /// <param name="quality">Quality of the Rule using Quality enum</param>
            public Rule(string name, string desc, Quality quality)
            {
                _name = name;
                _desc = desc;
                _quality = quality;
            }
        }

        Random random = new Random();

        List<string> guilds = new List<string>()
        {
            "Azorius",
            "Boros",
            "Golgari",
            "Izzet",
            "Dimir",
            "Selesnya",
            "Simic",
            "Rakdos",
            "Orzhov",
            "Gruul",
            "Guildless",
            "None"
        };

        List<string> eras = new List<string>()
        {
            "Before Guilds",
            "Pre-War",
            "Decamilennial",
            "Golden Era",
            "Stable Era",
            "Pre-Reforging",
            "Post-Reforging",
            "Guild Collapse",
            "The Final Days"
        };

        List<string> players = new List<string>()
        {
            "Player A",
            "Player B",
            "Player C",
            "Player D",
            "Player E",
            "Player F",
            "Player G",
            "Player H"
        };

        List<string> races = new List<string>()
        {
            "Elf",
            "Half-Elf",
            "Dwarf",
            "Half-Orc",
            "Human",
            "Tiefling",
            "Dragonborn",
            "Gnome",
            "Halfling"
        };

        List<string> classes = new List<string>()
        {
            "Barbarian",
            "Bard",
            "Cleric",
            "Druid",
            "Fighter",
            "Monk",
            "Paladin",
            "Ranger",
            "Rogue",
            "Sorcerer",
            "Warlock",
            "Wizard"
        };

        List<Rule> rules = new List<Rule>()
        {
            new Rule("Old School Cool", "Hope you brought your dentures!\n\nEach character must be in it's 90th percentile for age", Quality.OK),
            new Rule("Zero Opportunity", "Too Slow!\n\nCreatures and players can't take attacks of opportunity", Quality.OK),
            new Rule("Edge Runner", "Like living life on the edge?\n\nEach character has 1hp but starts with double hit dice. Before each battle they may roll any number of hit dice to determine their starting hp for that battle", Quality.Bad),
            new Rule("Weave Deadzone", "The weave is weakening across Ravnica\n\nSpells have a 25% chance to fail upon casting", Quality.Bad),
            new Rule("Aetheric Boost", "The weave appears to have been boosted!\n\nSpells have a 25% chance to not consume a spell slot", Quality.Good),
            new Rule("Mana Burn", "The weave's been tainted!\n\nCasting a spell deals xd4 to the caster where x is the spell slot level used", Quality.Terrible),
            new Rule("Blink-182", "Here's one to make you say \"I Miss You\"!\n\nPlayers may teleport up to their movement speed to a location they can see in lieu of their movement in combat", Quality.Best),
            new Rule("Absolutely Smashed", "Uh oh, looks like you've been going at the wine too hard!\n\nPlayers attack with disadvantage but bludgeoning damage from melee attacks is doubled", Quality.Bad),
            new Rule("Brutal Criticals", "Smells like 'roid rage!\n\nCriticals deal max damage! For both players and enemies...", Quality.OK),
            new Rule("Memento Mori", "In life there are no second chances\n\nYou only get one death save 10+ survive, 1-9 die", Quality.Worst),
            new Rule("Reversal of Fate", "This one's for you Ben!\n\nNatural 1s are crits, natural 20s are critical fails", Quality.OK),
            new Rule("Pushed To The Limit", "Go beyond!\n\nYou can tap into an inner well of strength, each player gets an extra action surge per battle", Quality.Best),
            new Rule("Frail", "You need some milk!\n\nEach player gets -2 strength and -1 constitution", Quality.Terrible),
            new Rule("Achilles", "Bring all to heel!\n\nReplace 2 of your stats with an 18... and one with a 5", Quality.Good),
            new Rule("Velma Dinkley", "Jinkies!\n\nYou can only see 60ft in front of you", Quality.Bad),
            new Rule("Inner Eye", "You feel your sixth sense awaken!\n\nAll players have 10ft truesight", Quality.Good),
            new Rule("Circle of Violence", "Good day to be a Rakdos!\n\nWhen a creature or player lands a critical hit they may attack again. This effect stacks indefinitely", Quality.OK),
            new Rule("Do Ya Feel Lucky?", "Well do ya, Punk?\n\nLoot drops and quality vastly improved... but so does the chance of undetectably cursed items", Quality.OK),
            new Rule("What's an Armor Class?", "AC is just a myth right?\n\nAttacks against players are no longer tested against AC, instead a player chooses STR, CON or DEX and rolls a saving throw for that to determine if the attack hits", Quality.OK),
            new Rule("Point Buy", "Hope those weren't good stats you rolled!\n\nEveryone now uses point buy, starting stats are 8,8,8,8,8,8 with 27 points, 16 max in any stat with 14-16 costing 2 points not 1.", Quality.OK),
            new Rule("God-Mode", "*DM cries internally*\n\nReplace your lowest 3 rolls with 18s", Quality.Best),
            new Rule("Crit Lover", "No that's not a dirty pun shut up\n\nEveryone crits on an 18+. Everyone", Quality.OK),
            new Rule("Glass Bones and Paper Skin", "Not quite livin' like Larry!\n\nIf a player takes more than half their max HP from a single attack/spell they must make a CON save. On a fail they lose the remainder of their health", Quality.Worst),
            new Rule("Cowardice", "Fear... fear is the mind killer\n\nEvery time a player takes damage they must pass a Wisdom saving throw (DC equal to half damage taken rounded up). On a fail they drop their weapon and are paralyzed until their turn", Quality.Worst),
            new Rule("Death Claims All", "It comes for us all in the end\n\nEvery time a player takes damage they must pass a Constitution saving throw (DC equal to half damage taken rounded up. On a fail they fail a death save", Quality.Terrible),
            new Rule("Smoke and Mirrors", "Now you see me...\n\nPlayers can hide as a bonus action", Quality.Good),
            new Rule("Patience is a Virtue", "The blade that waits...\n\nIf a player does not use their action they may \"store\" an attack. Next time they attack they gain an extra attack for each one stored (max 3)", Quality.Good),
            new Rule("Cloak and Dagger", "Everybody gangsta 'til they got a dagger in their back\n\nEach player gets an extra 2d6 sneak attack dice plus the sneak attack feature if they don't already have it", Quality.Best),
            new Rule("Defensive Maneuvers", "Shields Up!\n\nAs long as a player has at least 1 ally within 5ft they gain +2 to their AC", Quality.Good),
            new Rule("Offensive Maneuvers", "Attack together!\n\nAs long as a player has at least 1 ally within 5ft they gain +2 to their attack rolls", Quality.Good),
            new Rule("Orientation", "My thoughts go out to shield users\n\nCombat is played with orientation rules. -5AC to attacks from behind, +3AC to attacks from the front, shields don't confer an AC bonus to attacks from behind", Quality.OK),
            new Rule("Broken Leg", "You gain Brouzouf!\n\nYour speed becomes 10ft", Quality.Terrible),
            new Rule("Blood God's Blessing", "Divine favour is yours!\n\nEach player gains \"Blood Smite\": They may use up to 3 hit dice after hitting with a melee attack. Roll those dice and add it as radiant damage to the total", Quality.Best),
            new Rule("Lucky", "Luck be a lady tonight!\n\nEach player gains the \"Lucky\" feat", Quality.Good),
            new Rule("Inspired", "A flash of genius\n\nEach player gains 3d8 inspiration", Quality.Good)
        };

        List<TextBox> specialRulesTextboxes = new List<TextBox>();
        List<Rule> chosenRules = new List<Rule>();

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Get color associated with a Rule using its Quality attribute
        /// </summary>
        /// <param name="rule"></param>
        /// <returns></returns>
        public Color getRuleColour(Rule rule)
        {
            switch (rule._quality)
            {
                case Quality.Worst:
                    return Color.Maroon;
                case Quality.Terrible:
                    return Color.Firebrick;
                case Quality.Bad:
                    return Color.LightPink;
                case Quality.OK:
                    return Color.White;
                case Quality.Good:
                    return Color.PaleGreen;
                case Quality.Best:
                    return Color.Green;
                default:
                    return Color.White;
            }
        }

        /// <summary>
        /// Rolls 4 6-sided dice, rerolls up to one 1 then returns the sum of the top three rolls
        /// </summary>
        /// <returns>Sum of the top three rolls</returns>
        public int statRoll()
        {
            List<int> rolls = new List<int>();
            for(int i = 0; i < 4; i++) { rolls.Add(random.Next(1, 7)); }                        // Roll 4 6-sided dice (4d6)
            if(rolls.Min() == 1) { rolls.Remove(rolls.Min()); rolls.Add(random.Next(1, 7)); }   // If lowest is a 1, reroll it
            rolls.Remove(rolls.Min());                                                          // Remove the lowest roll
            return rolls.Sum();                                                                 // Return the sum of the other 3 dice
        }

        /// <summary>
        /// Generate the Oneshot parameters and output them to the appropriate fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            short numPlayers = (short)nudPlayers.Value;
            short numRules = (short)nudRules.Value;

            #region Initialise All Output Fields and Lists
            txtAgainst.Clear();
            txtGuild.Clear();
            txtLevel.Clear();
            txtLocation.Clear();
            txtStatArray.Clear();
            lbxForcedChars.Items.Clear();
            lbxPlayers.Items.Clear();
            specialRulesTextboxes.ForEach(box => box.Dispose());
            specialRulesTextboxes.Clear();
            chosenRules.Clear();
            #endregion

            #region Set Players
            List<string> chosenPlayers = new List<string>();
            while(numPlayers > 0)
            {
                string player = players[random.Next(players.Count)];
                if (!chosenPlayers.Contains(player))
                {
                    chosenPlayers.Add(player);
                    lbxPlayers.Items.Add(player);
                    numPlayers--;
                }
            }
            #endregion

            #region Set Rules
            while (numRules > 0)
            {
                Rule chosenRule = rules[random.Next(0, rules.Count)];
                if(!chosenRules.Contains(chosenRule))
                {
                    chosenRules.Add(chosenRule);
                    TextBox newRule = new TextBox();
                    newRule.Text = chosenRule._name;
                    newRule.Location = new Point(215, 28 + ((chosenRules.Count - 1) * 32));
                    newRule.Size = new Size(212, 26);
                    newRule.Click += new EventHandler(specialRuleClicked);
                    newRule.Tag = chosenRule._desc;
                    newRule.Font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);
                    newRule.BackColor = getRuleColour(chosenRule);
                    if(new List<Quality>() { Quality.Worst, Quality.Terrible, Quality.Best}.Contains(chosenRule._quality)) { newRule.ForeColor = Color.White; }
                    specialRulesTextboxes.Add(newRule);
                    numRules--;
                }
                specialRulesTextboxes.ForEach(rule => { Controls.Add(rule); }); //Adds the textboxes to the form
            }
            #endregion

            #region Set Forced Characters
            List<string> forcedChars = new List<string>();
            // TODO: Reduce conversions. more conversions here than a damn rugby team
            int lim = (int)Math.Truncate((decimal)((short)nudPlayers.Value / 2)); // Maximum forced characters
            bool fin = false;
            while(!fin)
            {
                if (random.Next(1, 5) <= lim)
                {
                    int check = random.Next(1, 4);
                    if(check < 4) { lim--; }
                    if (check == 1) { forcedChars.Add(classes[random.Next(classes.Count)]); }
                    if (check == 2) { forcedChars.Add(races[random.Next(races.Count)]); }
                    if (check == 3) { forcedChars.Add(races[random.Next(races.Count)] + " " + classes[random.Next(classes.Count)]); }
                } else { fin = true; }
            }
            forcedChars.ForEach(chara => lbxForcedChars.Items.Add(chara));
            #endregion

            #region Populate Output Fields
            txtAgainst.Text = guilds[random.Next(guilds.Count)];
            txtGuild.Text = guilds[random.Next(guilds.Count)];
            txtLevel.Text = random.Next(1, 15).ToString();
            txtLocation.Text = $"Precinct {random.Next(1, 7).ToString()}";
            txtStatArray.Text = $"{statRoll()}, {statRoll()}, {statRoll()}, {statRoll()}, {statRoll()}, {statRoll()}";
            txtEra.Text = eras[random.Next(eras.Count)];
            #endregion
        }

        /// <summary>
        /// Show a MessageBox with a description of the special rule
        /// </summary>
        /// <remarks>
        /// Utilizes the Tag property of the TextBox object to get the rule description and the Text property to get the caption
        /// </remarks>
        /// <param name="sender">TextBox clicked</param>
        /// <param name="e"></param>
        public void specialRuleClicked(object sender, EventArgs e)
        {
            MessageBox.Show((string)((TextBox)sender).Tag, ((TextBox)sender).Text);
        }
    }
}
