using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using Unity.VisualScripting;
using ProjectG.Audio;

namespace ProjectG.Manager
{
    public class Leaderboard : MonoBehaviour
    {
        public static Leaderboard instance;

        [SerializeField]
        private List<TextMeshProUGUI> names;
        [SerializeField]
        private List<TextMeshProUGUI> scores;

        private string publicLeaderboardKey = //"946043153a2542a3cba68c5a76d59069e4dea542370ba7e21175ae677a1192e2";
                                              "40ae65e747285251ce9d29d8635c70a7504fdc74cd748879bbb34b5d5f49787d";
        string[] prohibitedWords;
        private bool updated = false;

        List<int> topScores;

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            GetEntries();
        }


        [ContextMenu("Get Entries")]
        public void GetEntries()
        {
            GameObject entries = GameObject.FindGameObjectWithTag("Entries");
            if (entries != null)
            {
                // Clear the lists to avoid duplicating entries if called multiple times.
                names.Clear();
                scores.Clear();

                int childCount = entries.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    Transform entryTransform = entries.transform.GetChild(i);
                    TextMeshProUGUI nameText = entryTransform.GetChild(1).GetComponent<TextMeshProUGUI>();
                    TextMeshProUGUI scoreText = entryTransform.GetChild(2).GetComponent<TextMeshProUGUI>();

                    if (nameText != null && scoreText != null)
                    {
                        names.Add(nameText);
                        scores.Add(scoreText);
                    }
                }
            }
        }

        public void GetLeaderboard()
        {
            updated = false;

            Debug.Log("Go");
            LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) => 
            {
                GetEntries();
                Debug.Log("Going");
                int loopLength = (msg.Length < 9) ? msg.Length : 9;
                for (int i = 0; i < loopLength; i++)
                {
                    try
                    {
                        names[i].text = msg[i].Username;
                    } catch { Debug.Log("No name"); }
                    try
                    {
                        scores[i].text = msg[i].Score.ToString();
                    }
                    catch { Debug.Log("No score"); }
                }
            });
            Debug.Log("Got!");
        }

        public int[] GetScore()
        {
            Debug.Log("Go");
            LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, (msg) =>
            {
                Debug.Log("Going");
                for (int i = 0; i < names.Count; i++)
                {
                    topScores.Add(msg[i].Score);
                }
            });
            Debug.Log("Got!");
            int[] topScoresArray = topScores.ToArray();
            return topScoresArray;
        }

        public void SetLeaderboardEntry(string username, int score)
        {
            Debug.Log("Yoink!");
            LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score,
                ((msg) => 
                {
                    Debug.Log("SHABLAMMO");
                    GetLeaderboard();
                }));
        }

        private void Start()
        {
            prohibitedWords = new string[]{
    "2 girls 1 cup",
    "2g1c",
    "4r5e",
    "5h1t",
    "5hit",
    "a_s_s",
    "a$$",
    "a$$hole",
    "a2m",
    "a54",
    "a55",
    "a55hole",
    "aeolus",
    "ahole",
    "alabama hot pocket",
    "alaskan pipeline",
    "anal",
    "anal impaler",
    "anal leakage",
    "analannie",
    "analprobe",
    "analsex",
    "anilingus",
    "anus",
    "apeshit",
    "ar5e",
    "areola",
    "areole",
    "arian",
    "arrse",
    "arse",
    "arsehole",
    "aryan",
    "ass",
    "ass fuck",
    "ass hole",
    "ass-fucker",
    "ass-hat",
    "ass-jabber",
    "ass-pirate",
    "assault",
    "assbag",
    "assbagger",
    "assbandit",
    "assbang",
    "assbanged",
    "assbanger",
    "assbangs",
    "assbite",
    "assblaster",
    "assclown",
    "asscock",
    "asscracker",
    "asses",
    "assface",
    "assfaces",
    "assfuck",
    "assfucker",
    "assfukka",
    "assgoblin",
    "assh0le",
    "asshat",
    "asshead",
    "assho1e",
    "asshole",
    "assholes",
    "asshopper",
    "asshore",
    "assjacker",
    "assjockey",
    "asskiss",
    "asskisser",
    "assklown",
    "asslick",
    "asslicker",
    "asslover",
    "assman",
    "assmaster",
    "assmonkey",
    "assmucus",
    "assmunch",
    "assmuncher",
    "assnigger",
    "asspacker",
    "asspirate",
    "asspuppies",
    "assranger",
    "assshit",
    "assshole",
    "asssucker",
    "asswad",
    "asswhore",
    "asswipe",
    "asswipes",
    "auto erotic",
    "autoerotic",
    "axwound",
    "azazel",
    "azz",
    "b!tch",
    "b00bs",
    "b17ch",
    "b1tch",
    "babe",
    "babeland",
    "babes",
    "baby batter",
    "baby juice",
    "badfuck",
    "ball gag",
    "ball gravy",
    "ball kicking",
    "ball licking",
    "ball sack",
    "ball sucking",
    "ballbag",
    "balllicker",
    "balls",
    "ballsack",
    "bampot",
    "bang",
    "bang (one's) box",
    "bangbros",
    "banger",
    "banging",
    "bareback",
    "barely legal",
    "barenaked",
    "barf",
    "barface",
    "barfface",
    "bastard",
    "bastardo",
    "bastards",
    "bastinado",
    "batty boy",
    "bawdy",
    "bazongas",
    "bazooms",
    "bbw",
    "bdsm",
    "beaner",
    "beaners",
    "beardedclam",
    "beastial",
    "beastiality",
    "beatch",
    "beater",
    "beatyourmeat",
    "beaver",
    "beaver cleaver",
    "beaver lips",
    "beef curtain",
    "beef curtains",
    "beer",
    "beeyotch",
    "bellend",
    "bender",
    "beotch",
    "bestial",
    "bestiality",
    "bi-sexual",
    "bi+ch",
    "biatch",
    "bicurious",
    "big black",
    "big breasts",
    "big knockers",
    "big tits",
    "bigbastard",
    "bigbutt",
    "bigger",
    "bigtits",
    "bimbo",
    "bimbos",
    "bint",
    "birdlock",
    "bisexual",
    "bitch",
    "bitch tit",
    "bitchass",
    "bitched",
    "bitcher",
    "bitchers",
    "bitches",
    "bitchez",
    "bitchin",
    "bitching",
    "bitchtits",
    "bitchy",
    "black cock",
    "blonde action",
    "blonde on blonde action",
    "bloodclaat",
    "bloody",
    "bloody hell",
    "blow",
    "blow job",
    "blow me",
    "blow mud",
    "blow your load",
    "blowjob",
    "blowjobs",
    "blue waffle",
    "blumpkin",
    "boang",
    "bod",
    "bodily",
    "bogan",
    "bohunk",
    "boink",
    "boiolas",
    "bollick",
    "bollock",
    "bollocks",
    "bollok",
    "bollox",
    "bomd",
    "bondage",
    "bone",
    "boned",
    "boner",
    "boners",
    "bong",
    "boob",
    "boobies",
    "boobs",
    "booby",
    "booger",
    "bookie",
    "boong",
    "boonga",
    "booobs",
    "boooobs",
    "booooobs",
    "booooooobs",
    "bootee",
    "bootie",
    "booty",
    "booty call",
    "booze",
    "boozer",
    "boozy",
    "bosom",
    "bosomy",
    "bowel",
    "bowels",
    "bra",
    "brassiere",
    "breast",
    "breastjob",
    "breastlover",
    "breastman",
    "breasts",
    "breeder",
    "brotherfucker",
    "brown showers",
    "brunette action",
    "buceta",
    "bugger",
    "buggered",
    "buggery",
    "bukkake",
    "bull shit",
    "bullcrap",
    "bulldike",
    "bulldyke",
    "bullet vibe",
    "bullshit",
    "bullshits",
    "bullshitted",
    "bullturds",
    "bum",
    "bum boy",
    "bumblefuck",
    "bumclat",
    "bumfuck",
    "bummer",
    "bung",
    "bung hole",
    "bunga",
    "bunghole",
    "bunny fucker",
    "bust a load",
    "busty",
    "butchdike",
    "butchdyke",
    "butt",
    "butt fuck",
    "butt plug",
    "butt-bang",
    "butt-fuck",
    "butt-fucker",
    "butt-pirate",
    "buttbang",
    "buttcheeks",
    "buttface",
    "buttfuck",
    "buttfucka",
    "buttfucker",
    "butthead",
    "butthole",
    "buttman",
    "buttmuch",
    "buttmunch",
    "buttmuncher",
    "buttplug",
    "c-0-c-k",
    "c-o-c-k",
    "c-u-n-t",
    "c.0.c.k",
    "c.o.c.k.",
    "c.u.n.t",
    "c0ck",
    "c0cksucker",
    "caca",
    "cahone",
    "camel toe",
    "cameltoe",
    "camgirl",
    "camslut",
    "camwhore",
    "carpet muncher",
    "carpetmuncher",
    "cawk",
    "cervix",
    "chesticle",
    "chi-chi man",
    "chick with a dick",
    "child-fucker",
    "chin",
    "chinc",
    "chincs",
    "chink",
    "chinky",
    "choad",
    "choade",
    "choc ice",
    "chocolate rosebuds",
    "chode",
    "chodes",
    "chota bags",
    "cipa",
    "circlejerk",
    "cl1t",
    "cleveland steamer",
    "climax",
    "clit",
    "clit licker",
    "clitface",
    "clitfuck",
    "clitoris",
    "clitorus",
    "clits",
    "clitty",
    "clitty litter",
    "clogwog",
    "clover clamps",
    "clunge",
    "clusterfuck",
    "cnut",
    "cocain",
    "cocaine",
    "cock",
    "cock pocket",
    "cock snot",
    "cock sucker",
    "cockass",
    "cockbite",
    "cockblock",
    "cockburger",
    "cockeye",
    "cockface",
    "cockfucker",
    "cockhead",
    "cockholster",
    "cockjockey",
    "cockknocker",
    "cockknoker",
    "cocklicker",
    "cocklover",
    "cocklump",
    "cockmaster",
    "cockmongler",
    "cockmongruel",
    "cockmonkey",
    "cockmunch",
    "cockmuncher",
    "cocknose",
    "cocknugget",
    "cocks",
    "cockshit",
    "cocksmith",
    "cocksmoke",
    "cocksmoker",
    "cocksniffer",
    "cocksucer",
    "cocksuck",
    "cocksuck",
    "cocksucked",
    "cocksucker",
    "cocksuckers",
    "cocksucking",
    "cocksucks",
    "cocksuka",
    "cocksukka",
    "cockwaffle",
    "coffin dodger",
    "coital",
    "cok",
    "cokmuncher",
    "coksucka",
    "commie",
    "condom",
    "coochie",
    "coochy",
    "coon",
    "coonnass",
    "coons",
    "cooter",
    "cop some wood",
    "coprolagnia",
    "coprophilia",
    "corksucker",
    "cornhole",
    "corp whore",
    "cox",
    "crabs",
    "crack",
    "crack-whore",
    "cracker",
    "crackwhore",
    "crap",
    "crappy",
    "creampie",
    "cretin",
    "crikey",
    "cripple",
    "crotte",
    "cum",
    "cum chugger",
    "cum dumpster",
    "cum freak",
    "cum guzzler",
    "cumbubble",
    "cumdump",
    "cumdumpster",
    "cumguzzler",
    "cumjockey",
    "cummer",
    "cummin",
    "cumming",
    "cums",
    "cumshot",
    "cumshots",
    "cumslut",
    "cumstain",
    "cumtart",
    "cunilingus",
    "cunillingus",
    "cunn",
    "cunnie",
    "cunnilingus",
    "cunntt",
    "cunny",
    "cunt",
    "cunt hair",
    "cunt-struck",
    "cuntass",
    "cuntbag",
    "cuntface",
    "cuntfuck",
    "cuntfucker",
    "cunthole",
    "cunthunter",
    "cuntlick",
    "cuntlick",
    "cuntlicker",
    "cuntlicker",
    "cuntlicking",
    "cuntrag",
    "cunts",
    "cuntsicle",
    "cuntslut",
    "cuntsucker",
    "cut rope",
    "cyalis",
    "cyberfuc",
    "cyberfuck",
    "cyberfucked",
    "cyberfucker",
    "cyberfuckers",
    "cyberfucking",
    "cybersex",
    "d0ng",
    "d0uch3",
    "d0uche",
    "d1ck",
    "d1ld0",
    "d1ldo",
    "dago",
    "dagos",
    "dammit",
    "damn",
    "damned",
    "damnit",
    "darkie",
    "darn",
    "date rape",
    "daterape",
    "dawgie-style",
    "deep throat",
    "deepthroat",
    "deggo",
    "dendrophilia",
    "dick",
    "dick head",
    "dick hole",
    "dick shy",
    "dick-ish",
    "dick-sneeze",
    "dickbag",
    "dickbeaters",
    "dickbrain",
    "dickdipper",
    "dickface",
    "dickflipper",
    "dickfuck",
    "dickfucker",
    "dickhead",
    "dickheads",
    "dickhole",
    "dickish",
    "dickjuice",
    "dickmilk",
    "dickmonger",
    "dickripper",
    "dicks",
    "dicksipper",
    "dickslap",
    "dicksucker",
    "dicksucking",
    "dicktickler",
    "dickwad",
    "dickweasel",
    "dickweed",
    "dickwhipper",
    "dickwod",
    "dickzipper",
    "diddle",
    "dike",
    "dildo",
    "dildos",
    "diligaf",
    "dillweed",
    "dimwit",
    "dingle",
    "dingleberries",
    "dingleberry",
    "dink",
    "dinks",
    "dipship",
    "dipshit",
    "dirsa",
    "dirty",
    "dirty pillows",
    "dirty sanchez",
    "dlck",
    "dog style",
    "dog-fucker",
    "doggie style",
    "doggie-style",
    "doggiestyle",
    "doggin",
    "dogging",
    "doggy style",
    "doggy-style",
    "doggystyle",
    "dolcett",
    "domination",
    "dominatrix",
    "dommes",
    "dong",
    "donkey punch",
    "donkeypunch",
    "donkeyribber",
    "doochbag",
    "doofus",
    "dookie",
    "doosh",
    "dopey",
    "double dong",
    "double penetration",
    "doublelift",
    "douch3",
    "douche",
    "douche-fag",
    "douchebag",
    "douchebags",
    "douchewaffle",
    "douchey",
    "dp action",
    "drunk",
    "dry hump",
    "duche",
    "dumass",
    "dumb ass",
    "dumbass",
    "dumbasses",
    "dumbcunt",
    "dumbfuck",
    "dumbshit",
    "dummy",
    "dumshit",
    "dvda",
    "dyke",
    "dykes",
    "eat a dick",
    "eat hair pie",
    "eat my ass",
    "eatpussy",
    "ecchi",
    "ejaculate",
    "ejaculated",
    "ejaculates",
    "ejaculating",
    "ejaculatings",
    "ejaculation",
    "ejakulate",
    "enlargement",
    "erect",
    "erection",
    "erotic",
    "erotism",
    "escort",
    "essohbee",
    "eunuch",
    "extacy",
    "extasy",
    "f u c k",
    "f u c k e r",
    "f_u_c_k",
    "f-u-c-k",
    "f.u.c.k",
    "f4nny",
    "facefucker",
    "facial",
    "fack",
    "fag",
    "fagbag",
    "fagfucker",
    "fagg",
    "fagged",
    "fagging",
    "faggit",
    "faggitt",
    "faggot",
    "faggotcock",
    "faggots",
    "faggs",
    "fagot",
    "fagots",
    "fags",
    "fagtard",
    "faig",
    "faigt",
    "fanny",
    "fannybandit",
    "fannyflaps",
    "fannyfucker",
    "fanyy",
    "fart",
    "fartknocker",
    "fastfuck",
    "fat",
    "fatass",
    "fatfuck",
    "fatfucker",
    "fcuk",
    "fcuker",
    "fcuking",
    "fecal",
    "feck",
    "fecker",
    "felch",
    "felcher",
    "felching",
    "fellate",
    "fellatio",
    "feltch",
    "feltcher",
    "female squirting",
    "femdom",
    "fenian",
    "figging",
    "fingerbang",
    "fingerfuck",
    "fingerfuck",
    "fingerfucked",
    "fingerfucker",
    "fingerfucker",
    "fingerfuckers",
    "fingerfucking",
    "fingerfucks",
    "fingering",
    "fist fuck",
    "fisted",
    "fistfuck",
    "fistfucked",
    "fistfucker",
    "fistfucker",
    "fistfuckers",
    "fistfucking",
    "fistfuckings",
    "fistfucks",
    "fisting",
    "fisty",
    "flamer",
    "flange",
    "flaps",
    "fleshflute",
    "flog the log",
    "floozy",
    "foad",
    "foah",
    "fondle",
    "foobar",
    "fook",
    "fooker",
    "foot fetish",
    "footfuck",
    "footfucker",
    "footjob",
    "footlicker",
    "foreskin",
    "freakfuck",
    "freakyfucker",
    "freefuck",
    "freex",
    "frigg",
    "frigga",
    "frotting",
    "fubar",
    "fuc",
    "fuck",
    "fuck buttons",
    "fuck hole",
    "fuck off",
    "fuck puppet",
    "fuck trophy",
    "fuck yo mama",
    "fuck you",
    "fuck-ass",
    "fuck-bitch",
    "fuck-tard",
    "fucka",
    "fuckass",
    "fuckbag",
    "fuckboy",
    "fuckbrain",
    "fuckbutt",
    "fuckbutter",
    "fucked",
    "fuckedup",
    "fucker",
    "fuckers",
    "fuckersucker",
    "fuckface",
    "fuckfreak",
    "fuckhead",
    "fuckheads",
    "fuckher",
    "fuckhole",
    "fuckin",
    "fucking",
    "fuckingbitch",
    "fuckings",
    "fuckingshitmotherfucker",
    "fuckme",
    "fuckme",
    "fuckmeat",
    "fuckmehard",
    "fuckmonkey",
    "fucknugget",
    "fucknut",
    "fucknutt",
    "fuckoff",
    "fucks",
    "fuckstick",
    "fucktard",
    "fucktards",
    "fucktart",
    "fucktoy",
    "fucktwat",
    "fuckup",
    "fuckwad",
    "fuckwhit",
    "fuckwhore",
    "fuckwit",
    "fuckwitt",
    "fuckyou",
    "fudge packer",
    "fudge-packer",
    "fudgepacker",
    "fuk",
    "fuker",
    "fukker",
    "fukkers",
    "fukkin",
    "fuks",
    "fukwhit",
    "fukwit",
    "fuq",
    "futanari",
    "fux",
    "fux0r",
    "fvck",
    "fxck",
    "g-spot",
    "gae",
    "gai",
    "gang bang",
    "gang-bang",
    "gangbang",
    "gangbanged",
    "gangbangs",
    "ganja",
    "gash",
    "gassy ass",
    "gay sex",
    "gayass",
    "gaybob",
    "gaydo",
    "gayfuck",
    "gayfuckist",
    "gaylord",
    "gays",
    "gaysex",
    "gaytard",
    "gaywad",
    "gender bender",
    "genitals",
    "gey",
    "gfy",
    "ghay",
    "ghey",
     };
        }

    }
}