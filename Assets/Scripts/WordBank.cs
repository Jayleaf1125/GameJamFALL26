using System.Collections.Generic;

public static class Words
{
  public const string RALEWAY = "Raleway";
  public const string TIMES = "Times New Roman";
  public const string MONTSERRAT = "Montserrat";
  public const string POPPINS = "Poppins";
  public const string CAVEAT = "Caveat";
  public const string LOBSTER = "Lobster";
  public const string PACIFICO = "Pacifico";
  public const string DELAFIELD = "Mrs Saint Delafield";
  public const string MONSIEUR = "Monsieur La Doulaise";
  public const string PLAYWRITE = "Playwrite DE LA";
  
  public static Dictionary<int, string[]> Bank { get; private set; } = new Dictionary<int, string[]>()
  {
    { 
      3, 
      new string[] 
      { 
        "ten",
        "red",
        "ear",
        "end",
        "far",
        "lab",
        "bum",
        "sue",
        "run",
        "ugh",
        "zit",
        "zap",
        "yum",
        "raw",
        "aid",
      }
    },
    { 
      4, 
      new string[] 
      { 
        "fish",
        "dish",
        "lamb",
        "atom",
        "auto",
        "even",
        "cuny",
        "flag",
        "fall",
        "yoke",
        "yeti",
        "yale",
        "city",
        "doom",
        "ten",
      }
    },
    { 
      5, 
      new string[] 
      { 
        "harsh",
        "badge",
        "cahow",
        "sword",
        "spear",
        "fable",
        "japan",
        "idiom",
        "gamba",
        "idiot",
        "squid",
        "hotel",
        "match",
        "macho",
        "nacho",
      }
    },
    { 
      6, 
      new string[] 
      { 
        "burger",
        "hostel",
        "disney",
        "begger",
        "velvet",
        "mogged",
        "heresy",
        "lackey",
        "napalm",
        "quaggy",
        "wagon",
        "pickle",
        "decade",
        "jiggle",
        "xyloid",
      }
    },
    { 
      7, 
      new string[] 
      { 
        "decagon",
        "tenfold",
        "quantum",
        "coconut",
        "divorce",
        "ambient",
        "backlog",
        "cabbing",
        "cabbage",
        "science",
        "iframes",
        "habitus",
        "quarrel",
        "marshak",
        "million",
      }
    },
    { 
      8, 
      new string[] 
      { 
        "outbreak",
        "backcast",
        "backbone",
        "earnings",
        "earmuffs",
        "facility",
        "fadeaway",
        "galaxies",
        "vicinity",
        "nailhead",
        "macaroon",
        "nonsense",
        "wireless",
        "portable",
        "voidness",
      }
    },
    { 
      9, 
      new string[] 
      { 
        "decennial",
        "blasphemy",
        "newsflash",
        "neverland",
        "dystopian",
        "defective",
        "brimstone",
        "persecute",
        "incorrect",
        "immovable",
        "jailhouse",
        "knowledge",
        "decillion",
        "rejection",
        "developer",
      }
    },
    { 
      10, 
      new string[] 
      { 
        "iconoscope",
        "narcolepsy",
        "nannyberry",
        "inquisitor",
        "salmonella",
        "hyperacute",
        "ideamonger",
        "audiophile",
        "gingivitis",
        "pennyworth",
        "ecosystems",
        "gamekeeper",
        "terrifying",
        "collective",
        "faintheart",
      }
    },
  };
}
