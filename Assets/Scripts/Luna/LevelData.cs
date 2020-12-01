namespace Luna
{
    public class LevelData
    {
        public const char CHUNK_CHAR = 'x';
        public const int CHUNK_WIDTH = 4;
        public const int CHUNK_HEIGHT = 3;


        public static LevelDefinition[] Levels =
        {
            new LevelDefinition
            {
                Level =
                "ccc___t__" +
                "cc__wwwww" +
                "c___cc_cc" +
                "c___c0000" +
                "cc__c0000" +
                "cc__cx000" +
                "cc______r" +
                "c___wwwww" +
                "c_______w" +
                "0000cc__w" +
                "0000___cc" +
                "x000b_ccc",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "0000ctccc" +
                    "0000___cc" +
                    "x000____c" +
                    "cc___w___" +
                    "cc__ww___" +
                    "___ccw___" +
                    "__0000__r" +
                    "__0000___" +
                    "__x000_cc" +
                    "l__ww0000" +
                    "wwwww0000" +
                    "ccccwx000",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "ntn______" +
                    "_n_______" +
                    "wwww___ww" +
                    "_________" +
                    "___0000__" +
                    "n__0000__" +
                    "ln_x000__" +
                    "n________" +
                    "__0000___" +
                    "__0000___" +
                    "__x000nn_" +
                    "c__wwnbn_",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "_0000c_nt" +
                    "n0000c_nn" +
                    "lx000c___" +
                    "nn__c___" +
                    "_________" +
                    "c__www__c" +
                    "c__chc__c" +
                    "c__www__c" +
                    "_________" +
                    "0000_____" +
                    "0000nnn_c" +
                    "x000_b_cc",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "___nt0000" +
                    "___nn0000" +
                    "_____x000" +
                    "__c______" +
                    "__c0000_n" +
                    "__c0000nr" +
                    "w__x000ww" +
                    "w_______w" +
                    "ww_______" +
                    "____0000_" +
                    "____0000_" +
                    "_b__x000h",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "wntn_0000" +
                    "w_n__0000" +
                    "w____x000" +
                    "0000___wh" +
                    "0000___cc" +
                    "x000____c" +
                    "w_____w__" +
                    "w__0000__" +
                    "ww_0000nn" +
                    "w__x000nr" +
                    "c_n_ccccc" +
                    "cnbn_cccc",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "_ntn_____" +
                    "c_n0000__" +
                    "cc_0000__" +
                    "___x000__" +
                    "_________" +
                    "__0000__w" +
                    "__0000w_w" +
                    "__x000__w" +
                    "_________" +
                    "___0000_c" +
                    "_nn0000_c" +
                    "_nbx000hc",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },

            new LevelDefinition
            {
                Level =
                    "0000____n" +
                    "0000___nr" +
                    "x000____n" +
                    "________" +
                    "_c_c_0000" +
                    "_c_c_0000" +
                    "__w__x000" +
                    "__w__cccc" +
                    "__w____cc" +
                    "n____0000" +
                    "ln___0000" +
                    "cn___x000",

                MinEnemies = 3,
                MaxEnemies = 6,
                MinTerrain = 2,
                MaxTerrain = 5,
                MinItems = 2,
                MaxItems = 5,
            },
        };


        public static string[] Chunks =
        {
            "wwww" +
            "w_hw" +
            "wwww",

            "wwww" +
            "wccw" +
            "wwww",

            "cwwc" +
            "ccwc" +
            "cwwc",

            "cwww" +
            "cwhc" +
            "cwwc",

            "cw_w" +
            "ch_c" +
            "c_wc",

            "____" +
            "cc__" +
            "___c",

            "www_" +
            "____" +
            "_www",

            "____" +
            "_cc_" +
            "__c_",

            "_ww_" +
            "_hw_" +
            "__w_",

            "wwww" +
            "___w" +
            "_www",

            "wwww" +
            "__hw" +
            "_www",

            "cw_c" +
            "c__c" +
            "c_wc",

            "cccc" +
            "w_h_" +
            "w_c_",


            "__w_" +
            "_ww_" +
            "_w__",

            "wwww" +
            "_hw_" +
            "www_",

            "wwww" +
            "_cc_" +
            "____",

            "__cc" +
            "_ccc" +
            "__cc",

            "cwww" +
            "cchw" +
            "cwww",

            "cccc" +
            "ch_c" +
            "ccwc"
        };
    }
}