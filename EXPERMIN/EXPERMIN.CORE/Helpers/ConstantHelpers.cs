using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXPERMIN.CORE.Helpers
{
    public static class ConstantHelpers
    {

        public const string PROJECT = "Expermin";
        public const string URL_EMAIL_LOGO = "";
        public static class GENERAL
        {
            public const string PROJECT_URI_BASE = "https://expermin.pe";
            public static class FILESTORAGE
            {
                public const string PATH = "/home/www/files";
            }
        }
        public static class ROLES
        {   //string adminString = ConstantHelpers.ROLES.ADMIN;
            //Console.WriteLine(adminString); // Output: "Administrador"
            public const string ADMIN = "Administrador";
            public const string CLIENT = "Cliente";
            public static Dictionary<int, string> INDICES = new Dictionary<int, string>()
            {   
                //int adminInt = ConstantHelpers.ROLES.INDICES.FirstOrDefault(x => x.Value == ConstantHelpers.ROLES.ADMIN).Key;
                //Console.WriteLine(adminInt); // Output: 1
                { 1, ADMIN },
                { 2, CLIENT }
            };
        }
        public static class USER
        {
            public static class SEX
            {
                public const byte MALE = 1;
                public const byte FERMALE = 2;
                public const byte UNSPECIFIED = 3;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>()
                    {
                        { MALE, "Masculino" },
                        { FERMALE, "Femenino" },
                        { UNSPECIFIED, "Sin Especificar" },
                    };
            }

            public static class REGISTER_BY
            {
                public const byte LC = 1;
                public const byte GOOGLE = 2;
                public const byte FACEBOOK = 3;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>()
                    {
                        { LC, "Legal Connection" },
                        { GOOGLE, "Google" },
                        { FACEBOOK, "Facebook" },
                    };
            }

        }
        public static class BANNER
        {
            public class STATUS
            {
                public const byte ACTIVE = 1;
                public const byte HIDDEN = 2;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                    {
                        { ACTIVE, "Activo" },
                        { HIDDEN, "Oculto" }
                    };
            }

            public class TYPE
            {
                public const byte INTERNAL = 1;
                public const byte EXTERNAL = 2;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                    {
                        { INTERNAL, "Interna" },
                        { EXTERNAL, "Externa" }
                    };
            }
        }
        public static class SECTION_ITEMS
        {
            public class TYPES
            {
                //public const byte BENEFITS = 1;
                public const byte PRODUCTS = 2;
                public const byte ABAOUT_US = 3;
                public const byte TESTIMONIES = 4;
                public const byte OUR_CLIENT = 5;

                public static Dictionary<byte, string> VALUES = new Dictionary<byte, string>
                    {
                        //{ BENEFITS, "Beneficios" },
                        { PRODUCTS, "Productos" },
                        { ABAOUT_US, "Nosotros" },
                        { TESTIMONIES, "Testimonios" },
                        { OUR_CLIENT, "Nuestros Clientes" },
                    };
            }
        }
        public static class FREQUENT_QUESTION
        {
            public class ICONS
            {
                //public const int ICON_1 = 1;
                //public const int ICON_2 = 2;
                //public const int ICON_3 = 3;
                //public const int ICON_4 = 4;
                public const int ICON_5 = 5;
                //public const int ICON_6 = 6;
                //public const int ICON_7 = 7;
                public const int ICON_8 = 8;
                public const int ICON_9 = 9;
                //public const int ICON_10 = 10;
                //public const int ICON_11 = 11;
                //public const int ICON_12 = 12;
                //public const int ICON_13 = 13;
                //public const int ICON_14 = 14;
                public const int ICON_15 = 15;
                public const int ICON_16 = 16;
                public const int ICON_17 = 17;
                //public const int ICON_18 = 18;
                public const int ICON_19 = 19;
                public const int ICON_20 = 20;
                public const int ICON_21 = 21;
                public const int ICON_22 = 22;
                public const int ICON_23 = 23;
                public const int ICON_24 = 24;
                public const int ICON_25 = 25;
                //public const int ICON_26 = 26;
                //public const int ICON_27 = 27;
                //public const int ICON_28 = 28;
                //public const int ICON_29 = 29;
                //public const int ICON_30 = 30;
                //public const int ICON_31 = 31;
                //public const int ICON_32 = 32;
                //public const int ICON_33 = 33;
                //public const int ICON_34 = 34;
                //public const int ICON_35 = 35;
                //public const int ICON_36 = 36;
                //public const int ICON_37 = 37;
                //public const int ICON_38 = 38;
                //public const int ICON_39 = 39;
                //public const int ICON_40 = 40;
                //public const int ICON_41 = 41;
                //public const int ICON_42 = 42;
                //public const int ICON_43 = 43;
                public const int ICON_44 = 44;
                public const int ICON_45 = 45;
                public const int ICON_46 = 46;
                //public const int ICON_47 = 47;
                public const int ICON_48 = 48;
                public const int ICON_49 = 49;
                public const int ICON_50 = 50;
                public const int ICON_51 = 51;
                public const int ICON_52 = 52;
                //public const int ICON_53 = 53;
                public const int ICON_54 = 54;
                public const int ICON_55 = 55;
                public const int ICON_56 = 56;
                public const int ICON_57 = 57;
                public const int ICON_58 = 58;
                public const int ICON_59 = 59;
                public const int ICON_60 = 60;
                public const int ICON_61 = 61;
                public const int ICON_62 = 62;
                public const int ICON_63 = 63;
                //public const int ICON_64 = 64;
                //public const int ICON_65 = 65;
                //public const int ICON_66 = 66;
                //public const int ICON_67 = 67;
                //public const int ICON_68 = 68;
                public const int ICON_69 = 69;
                public const int ICON_70 = 70;
                public const int ICON_71 = 71;
                public const int ICON_72 = 72;
                public const int ICON_73 = 73;
                public const int ICON_74 = 74;
                public const int ICON_75 = 75;
                public const int ICON_76 = 76;
                public const int ICON_77 = 77;
                public const int ICON_78 = 78;
                public const int ICON_79 = 79;
                public const int ICON_80 = 80;
                public const int ICON_81 = 81;
                public const int ICON_82 = 82;
                public const int ICON_83 = 83;
                public const int ICON_84 = 84;
                //public const int ICON_85 = 85;
                public const int ICON_86 = 86;
                //public const int ICON_87 = 87;
                //public const int ICON_88 = 88;
                //public const int ICON_89 = 89;
                public const int ICON_90 = 90;
                public const int ICON_91 = 91;
                public const int ICON_92 = 92;
                public const int ICON_93 = 93;
                public const int ICON_94 = 94;
                public const int ICON_95 = 95;
                public const int ICON_96 = 96;
                //public const int ICON_97 = 97;
                public const int ICON_98 = 98;
                public const int ICON_99 = 99;
                public const int ICON_100 = 100;
                public const int ICON_101 = 101;
                //public const int ICON_102 = 102;
                public const int ICON_103 = 103;
                //public const int ICON_104 = 104;
                public const int ICON_105 = 105;
                //public const int ICON_106 = 106;
                public const int ICON_107 = 107;
                public const int ICON_108 = 108;
                public const int ICON_109 = 109;
                //public const int ICON_110 = 110;
                public const int ICON_111 = 111;
                //public const int ICON_112 = 112;
                //public const int ICON_113 = 113;
                //public const int ICON_114 = 114;
                //public const int ICON_115 = 115;
                //public const int ICON_116 = 116;
                public const int ICON_117 = 117;
                public const int ICON_118 = 118;
                //public const int ICON_119 = 119;
                //public const int ICON_120 = 120;
                public const int ICON_121 = 121;
                public const int ICON_122 = 122;
                //public const int ICON_123 = 123;
                public const int ICON_124 = 124;
                //public const int ICON_125 = 125;
                //public const int ICON_126 = 126;
                public const int ICON_127 = 127;
                //public const int ICON_128 = 128;
                //public const int ICON_129 = 129;
                public const int ICON_130 = 130;
                //public const int ICON_131 = 131;
                //public const int ICON_132 = 132;
                //public const int ICON_133 = 133;
                public const int ICON_134 = 134;
                public const int ICON_135 = 135;
                public const int ICON_136 = 136;
                public const int ICON_137 = 137;
                public const int ICON_138 = 138;
                public const int ICON_139 = 139;
                public const int ICON_140 = 140;
                public const int ICON_141 = 141;
                public const int ICON_142 = 142;
                public const int ICON_143 = 143;
                public const int ICON_144 = 144;
                public const int ICON_145 = 145;
                //public const int ICON_146 = 146;
                //public const int ICON_147 = 147;
                //public const int ICON_148 = 148;
                //public const int ICON_149 = 149;
                //public const int ICON_150 = 150;
                //public const int ICON_151 = 151;
                //public const int ICON_152 = 152;
                //public const int ICON_153 = 153;
                //public const int ICON_154 = 154;
                //public const int ICON_155 = 155;
                //public const int ICON_156 = 156;
                //public const int ICON_157 = 157;
                //public const int ICON_158 = 158;
                //public const int ICON_159 = 159;
                //public const int ICON_160 = 160;
                //public const int ICON_161 = 161;
                //public const int ICON_162 = 162;
                public const int ICON_163 = 163;
                //public const int ICON_164 = 164;
                //public const int ICON_165 = 165;
                //public const int ICON_166 = 166;
                //public const int ICON_167 = 167;
                //public const int ICON_168 = 168;
                public const int ICON_169 = 169;
                public const int ICON_170 = 170;
                public const int ICON_171 = 171;
                //public const int ICON_172 = 172;
                public const int ICON_173 = 173;
                //public const int ICON_174 = 174;
                public const int ICON_175 = 175;
                //public const int ICON_176 = 176;
                //public const int ICON_177 = 177;
                public const int ICON_178 = 178;
                public const int ICON_179 = 179;
                public const int ICON_180 = 180;
                //public const int ICON_181 = 181;
                //public const int ICON_182 = 182;
                //public const int ICON_183 = 183;
                //public const int ICON_184 = 184;
                public const int ICON_185 = 185;
                public const int ICON_186 = 186;
                //public const int ICON_187 = 187;
                //public const int ICON_188 = 188;
                //public const int ICON_189 = 189;
                //public const int ICON_190 = 190;
                //public const int ICON_191 = 191;
                //public const int ICON_192 = 192;
                //public const int ICON_193 = 193;
                //public const int ICON_194 = 194;
                public const int ICON_195 = 195;
                public const int ICON_196 = 196;
                //public const int ICON_197 = 197;
                public const int ICON_198 = 198;
                public const int ICON_199 = 199;
                public const int ICON_200 = 200;
                //public const int ICON_201 = 201;
                public const int ICON_202 = 202;
                //public const int ICON_203 = 203;
                //public const int ICON_204 = 204;
                //public const int ICON_205 = 205;
                //public const int ICON_206 = 206;
                //public const int ICON_207 = 207;
                //public const int ICON_208 = 208;
                //public const int ICON_209 = 209;
                public const int ICON_210 = 210;
                //public const int ICON_211 = 211;
                public const int ICON_212 = 212;
                public const int ICON_213 = 213;
                public const int ICON_214 = 214;
                public const int ICON_215 = 215;
                public const int ICON_216 = 216;
                //public const int ICON_217 = 217;
                public const int ICON_218 = 218;
                //public const int ICON_219 = 219;
                //public const int ICON_220 = 220;
                //public const int ICON_221 = 221;
                public const int ICON_222 = 222;
                public const int ICON_223 = 223;
                public const int ICON_224 = 224;
                //public const int ICON_225 = 225;
                //public const int ICON_226 = 226;
                //public const int ICON_227 = 227;
                //public const int ICON_228 = 228;
                public const int ICON_229 = 229;
                public const int ICON_230 = 230;
                //public const int ICON_231 = 231;
                public const int ICON_232 = 232;
                //public const int ICON_233 = 233;
                //public const int ICON_234 = 234;
                public const int ICON_235 = 235;
                public const int ICON_236 = 236;
                public const int ICON_237 = 237;
                public const int ICON_238 = 238;
                //public const int ICON_239 = 239;
                public const int ICON_240 = 240;
                //public const int ICON_241 = 241;
                //public const int ICON_242 = 242;
                //public const int ICON_243 = 243;
                //public const int ICON_244 = 244;
                //public const int ICON_245 = 245;
                //public const int ICON_246 = 246;
                //public const int ICON_247 = 247;
                //public const int ICON_248 = 248;
                //public const int ICON_249 = 249;
                //public const int ICON_250 = 250;
                //public const int ICON_251 = 251;
                //public const int ICON_252 = 252;
                //public const int ICON_253 = 253;
                //public const int ICON_254 = 254;
                public const int ICON_255 = 255;
                //public const int ICON_256 = 256;
                public const int ICON_257 = 257;
                //public const int ICON_258 = 258;
                //public const int ICON_259 = 259;
                //public const int ICON_260 = 260;
                //public const int ICON_261 = 261;
                //public const int ICON_262 = 262;
                //public const int ICON_263 = 263;
                //public const int ICON_264 = 264;
                //public const int ICON_265 = 265;
                //public const int ICON_266 = 266;
                //public const int ICON_267 = 267;
                //public const int ICON_268 = 268;
                public const int ICON_269 = 269;
                public const int ICON_270 = 270;
                //public const int ICON_271 = 271;
                //public const int ICON_272 = 272;
                public const int ICON_273 = 273;
                //public const int ICON_274 = 274;
                public const int ICON_275 = 275;
                //public const int ICON_276 = 276;
                public const int ICON_277 = 277;
                public const int ICON_278 = 278;
                //public const int ICON_279 = 279;
                //public const int ICON_280 = 280;
                //public const int ICON_281 = 281;
                //public const int ICON_282 = 282;
                //public const int ICON_283 = 283;
                //public const int ICON_284 = 284;
                //public const int ICON_285 = 285;
                //public const int ICON_286 = 286;
                //public const int ICON_287 = 287;
                //public const int ICON_288 = 288;
                public const int ICON_289 = 289;
                public const int ICON_290 = 290;
                //public const int ICON_291 = 291;
                public const int ICON_292 = 292;
                public const int ICON_293 = 293;
                public const int ICON_294 = 294;
                public const int ICON_295 = 295;
                public const int ICON_296 = 296;
                public const int ICON_297 = 297;
                public const int ICON_298 = 298;
                //public const int ICON_299 = 299;
                //public const int ICON_300 = 300;
                //public const int ICON_301 = 301;
                //public const int ICON_302 = 302;
                //public const int ICON_303 = 303;
                //public const int ICON_304 = 304;
                public const int ICON_305 = 305;
                //public const int ICON_306 = 306;
                //public const int ICON_307 = 307;
                public const int ICON_308 = 308;
                public const int ICON_309 = 309;
                public const int ICON_310 = 310;
                public const int ICON_311 = 311;
                //public const int ICON_312 = 312;
                //public const int ICON_313 = 313;
                //public const int ICON_314 = 314;
                //public const int ICON_315 = 315;
                //public const int ICON_316 = 316;
                //public const int ICON_317 = 317;
                //public const int ICON_318 = 318;
                public const int ICON_319 = 319;
                //public const int ICON_320 = 320;
                //public const int ICON_321 = 321;
                //public const int ICON_322 = 322;
                public const int ICON_323 = 323;
                //public const int ICON_324 = 324;
                public const int ICON_325 = 325;
                public const int ICON_326 = 326;
                //public const int ICON_327 = 327;
                public const int ICON_328 = 328;
                public const int ICON_329 = 329;
                //public const int ICON_330 = 330;
                //public const int ICON_331 = 331;
                //public const int ICON_332 = 332;
                //public const int ICON_333 = 333;
                public const int ICON_334 = 334;
                //public const int ICON_335 = 335;
                //public const int ICON_336 = 336;
                //public const int ICON_337 = 337;
                //public const int ICON_338 = 338;
                //public const int ICON_339 = 339;
                //public const int ICON_340 = 340;
                //public const int ICON_341 = 341;
                //public const int ICON_342 = 342;
                //public const int ICON_343 = 343;
                //public const int ICON_344 = 344;
                //public const int ICON_345 = 345;
                //public const int ICON_346 = 346;
                //public const int ICON_347 = 347;
                //public const int ICON_348 = 348;
                //public const int ICON_349 = 349;
                //public const int ICON_350 = 350;
                //public const int ICON_351 = 351;
                //public const int ICON_352 = 352;
                //public const int ICON_353 = 353;
                //public const int ICON_354 = 354;
                //public const int ICON_355 = 355;
                //public const int ICON_356 = 356;
                public const int ICON_357 = 357;
                //public const int ICON_358 = 358;
                public const int ICON_359 = 359;
                public const int ICON_360 = 360;
                //public const int ICON_361 = 361;
                //public const int ICON_362 = 362;
                //public const int ICON_363 = 363;
                //public const int ICON_364 = 364;
                public const int ICON_365 = 365;
                public const int ICON_366 = 366;
                public const int ICON_367 = 367;
                //public const int ICON_368 = 368;
                //public const int ICON_369 = 369;
                //public const int ICON_370 = 370;
                //public const int ICON_371 = 371;
                //public const int ICON_372 = 372;
                //public const int ICON_373 = 373;
                //public const int ICON_374 = 374;
                //public const int ICON_375 = 375;
                //public const int ICON_376 = 376;
                //public const int ICON_377 = 377;
                //public const int ICON_378 = 378;
                //public const int ICON_379 = 379;
                //public const int ICON_380 = 380;
                //public const int ICON_381 = 381;
                public const int ICON_382 = 382;
                public const int ICON_383 = 383;
                //public const int ICON_384 = 384;
                public const int ICON_385 = 385;
                //public const int ICON_386 = 386;
                public const int ICON_387 = 387;
                //public const int ICON_388 = 388;
                //public const int ICON_389 = 389;
                //public const int ICON_390 = 390;
                //public const int ICON_391 = 391;
                public const int ICON_392 = 392;
                //public const int ICON_393 = 393;
                //public const int ICON_394 = 394;
                //public const int ICON_395 = 395;
                //public const int ICON_396 = 396;
                //public const int ICON_397 = 397;
                //public const int ICON_398 = 398;
                public const int ICON_399 = 399;
                public const int ICON_400 = 400;
                //public const int ICON_401 = 401;
                public const int ICON_402 = 402;
                public const int ICON_403 = 403;
                //public const int ICON_404 = 404;
                //public const int ICON_405 = 405;
                //public const int ICON_406 = 406;
                //public const int ICON_407 = 407;
                //public const int ICON_408 = 408;
                //public const int ICON_409 = 409;
                //public const int ICON_410 = 410;
                //public const int ICON_411 = 411;
                //public const int ICON_412 = 412;
                public const int ICON_413 = 413;
                public const int ICON_414 = 414;
                //public const int ICON_415 = 415;
                //public const int ICON_416 = 416;
                //public const int ICON_417 = 417;
                //public const int ICON_418 = 418;
                //public const int ICON_419 = 419;
                //public const int ICON_420 = 420;
                public const int ICON_421 = 421;
                //public const int ICON_422 = 422;
                //public const int ICON_423 = 423;
                //public const int ICON_424 = 424;
                //public const int ICON_425 = 425;
                //public const int ICON_426 = 426;
                //public const int ICON_427 = 427;
                //public const int ICON_428 = 428;
                //public const int ICON_429 = 429;
                //public const int ICON_430 = 430;
                //public const int ICON_431 = 431;
                //public const int ICON_432 = 432;
                //public const int ICON_433 = 433;
                //public const int ICON_434 = 434;
                //public const int ICON_435 = 435;
                //public const int ICON_436 = 436;
                public const int ICON_437 = 437;
                public const int ICON_438 = 438;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                    {
                        //{ICON_1,"la la-address-book"},
                        //{ICON_2,"la la-address-book-o"},
                        //{ICON_3,"la la-address-card"},
                        //{ICON_4,"la la-address-card-o"},
                        {ICON_5,"la la-adjust"},
                        //{ICON_6,"la la-american-sign-language-interpreting"},
                        //{ICON_7,"la la-anchor"},
                        {ICON_8,"la la-archive"},
                        {ICON_9,"la la-area-chart"},
                        //{ICON_10,"la la-arrows"},
                        //{ICON_11,"la la-arrows-h"},
                        //{ICON_12,"la la-arrows-v"},
                        //{ICON_13,"la la-asl-interpreting"},
                        //{ICON_14,"la la-assistive-listening-systems"},
                        {ICON_15,"la la-asterisk"},
                        {ICON_16,"la la-at"},
                        {ICON_17,"la la-automobile"},
                        //{ICON_18,"la la-audio-description"},
                        {ICON_19,"la la-balance-scale"},
                        {ICON_20,"la la-ban"},
                        {ICON_21,"la la-bank"},
                        {ICON_22,"la la-bar-chart"},
                        {ICON_23,"la la-bar-chart-o"},
                        {ICON_24,"la la-barcode"},
                        {ICON_25,"la la-bars"},
                        //{ICON_26,"la la-bath"},
                        ////{ICON_27,"la la-bathtub"},
                        //{ICON_28,"la la-battery-0"},
                        //{ICON_29,"la la-battery-1"},
                        //{ICON_30,"la la-battery-2"},
                        //{ICON_31,"la la-battery-3"},
                        //{ICON_32,"la la-battery-4"},
                        //{ICON_33,"la la-battery-empty"},
                        //{ICON_34,"la la-battery-full"},
                        //{ICON_35,"la la-battery-half"},
                        //{ICON_36,"la la-battery-quarter"},
                        //{ICON_37,"la la-battery-three-quarters"},
                        //{ICON_38,"la la-bed"},
                        //{ICON_39,"la la-beer"},
                        //{ICON_40,"la la-bell"},
                        //{ICON_41,"la la-bell-o"},
                        //{ICON_42,"la la-bell-slash"},
                        //{ICON_43,"la la-bell-slash-o"},
                        {ICON_44,"la la-bicycle"},
                        {ICON_45,"la la-binoculars"},
                        {ICON_46,"la la-birthday-cake"},
                        //{ICON_47,"la la-blind"},
                        {ICON_48,"la la-bolt"},
                        {ICON_49,"la la-bomb"},
                        {ICON_50,"la la-book"},
                        {ICON_51,"la la-bookmark"},
                        {ICON_52,"la la-bookmark-o"},
                        //{ICON_53,"la la-braille"},
                        {ICON_54,"la la-briefcase"},
                        {ICON_55,"la la-bug"},
                        {ICON_56,"la la-building"},
                        {ICON_57,"la la-building-o"},
                        {ICON_58,"la la-bullhorn"},
                        {ICON_59,"la la-bullseye"},
                        {ICON_60,"la la-bus"},
                        {ICON_61,"la la-cab"},
                        {ICON_62,"la la-calculator"},
                        {ICON_63,"la la-calendar"},
                        //{ICON_64,"la la-calendar-o"},
                        //{ICON_65,"la la-calendar-check-o"},
                        //{ICON_66,"la la-calendar-minus-o"},
                        //{ICON_67,"la la-calendar-plus-o"},
                        //{ICON_68,"la la-calendar-times-o"},
                        {ICON_69,"la la-camera"},
                        {ICON_70,"la la-camera-retro"},
                        {ICON_71,"la la-car"},
                        {ICON_72,"la la-caret-square-o-down"},
                        {ICON_73,"la la-caret-square-o-left"},
                        {ICON_74,"la la-caret-square-o-right"},
                        {ICON_75,"la la-caret-square-o-up"},
                        {ICON_76,"la la-cart-arrow-down"},
                        {ICON_77,"la la-cart-plus"},
                        {ICON_78,"la la-cc"},
                        {ICON_79,"la la-certificate"},
                        {ICON_80,"la la-check"},
                        {ICON_81,"la la-check-circle"},
                        {ICON_82,"la la-check-circle-o"},
                        {ICON_83,"la la-check-square"},
                        {ICON_84,"la la-check-square-o"},
                        //{ICON_85,"la la-child"},
                        {ICON_86,"la la-circle"},
                        //{ICON_87,"la la-circle-o"},
                        //{ICON_88,"la la-circle-o-notch"},
                        //{ICON_89,"la la-circle-thin"},
                        {ICON_90,"la la-clock-o"},
                        {ICON_91,"la la-clone"},
                        {ICON_92,"la la-close"},
                        {ICON_93,"la la-cloud"},
                        {ICON_94,"la la-cloud-download"},
                        {ICON_95,"la la-cloud-upload"},
                        {ICON_96,"la la-code"},
                        //{ICON_97,"la la-code-fork"},
                        {ICON_98,"la la-coffee"},
                        {ICON_99,"la la-cog"},
                        {ICON_100,"la la-cogs"},
                        {ICON_101,"la la-comment"},
                        //{ICON_102,"la la-comment-o"},
                        {ICON_103,"la la-comments"},
                        //{ICON_104,"la la-comments-o"},
                        {ICON_105,"la la-commenting"},
                        //{ICON_106,"la la-commenting-o"},
                        {ICON_107,"la la-compass"},
                        {ICON_108,"la la-copyright"},
                        {ICON_109,"la la-credit-card"},
                        //{ICON_110,"la la-credit-card-alt"},
                        {ICON_111,"la la-creative-commons"},
                        //{ICON_112,"la la-crop"},
                        //{ICON_113,"la la-crosshairs"},
                        //{ICON_114,"la la-cube"},
                        //{ICON_115,"la la-cubes"},
                        //{ICON_116,"la la-cutlery"},
                        {ICON_117,"la la-dashboard"},
                        {ICON_118,"la la-database"},
                        //{ICON_119,"la la-deaf"},
                        //{ICON_120,"la la-deafness"},
                        {ICON_121,"la la-desktop"},
                        {ICON_122,"la la-diamond"},
                        //{ICON_123,"la la-dot-circle-o"},
                        {ICON_124,"la la-download"},
                        //{ICON_125,"la la-drivers-license"},
                        //{ICON_126,"la la-drivers-license-o"},
                        {ICON_127,"la la-edit"},
                        //{ICON_128,"la la-ellipsis-h"},
                        //{ICON_129,"la la-ellipsis-v"},
                        {ICON_130,"la la-envelope"},
                        //{ICON_131,"la la-envelope-o"},
                        //{ICON_132,"la la-envelope-open"},
                        //{ICON_133,"la la-envelope-open-o"},
                        {ICON_134,"la la-envelope-square"},
                        {ICON_135,"la la-eraser"},
                        {ICON_136,"la la-exchange"},
                        {ICON_137,"la la-exclamation"},
                        {ICON_138,"la la-exclamation-circle"},
                        {ICON_139,"la la-exclamation-triangle"},
                        {ICON_140,"la la-external-link"},
                        {ICON_141,"la la-external-link-square"},
                        {ICON_142,"la la-eye"},
                        {ICON_143,"la la-eye-slash"},
                        {ICON_144,"la la-eyedropper"},
                        {ICON_145,"la la-fax"},
                        //{ICON_146,"la la-female"},
                        //{ICON_147,"la la-fighter-jet"},
                        //{ICON_148,"la la-file-archive-o"},
                        //{ICON_149,"la la-file-audio-o"},
                        //{ICON_150,"la la-file-code-o"},
                        //{ICON_151,"la la-file-excel-o"},
                        //{ICON_152,"la la-file-image-o"},
                        //{ICON_153,"la la-file-movie-o"},
                        //{ICON_154,"la la-file-pdf-o"},
                        //{ICON_155,"la la-file-photo-o"},
                        //{ICON_156,"la la-file-picture-o"},
                        //{ICON_157,"la la-file-powerpoint-o"},
                        //{ICON_158,"la la-file-sound-o"},
                        //{ICON_159,"la la-file-video-o"},
                        //{ICON_160,"la la-file-word-o"},
                        //{ICON_161,"la la-file-zip-o"},
                        //{ICON_162,"la la-film"},
                        {ICON_163,"la la-filter"},
                        //{ICON_164,"la la-fire"},
                        //{ICON_165,"la la-fire-extinguisher"},
                        //{ICON_166,"la la-flag"},
                        //{ICON_167,"la la-flag-checkered"},
                        //{ICON_168,"la la-flag-o"},
                        {ICON_169,"la la-flash"},
                        {ICON_170,"la la-flask"},
                        {ICON_171,"la la-folder"},
                        //{ICON_172,"la la-folder-o"},
                        {ICON_173,"la la-folder-open"},
                        //{ICON_174,"la la-folder-open-o"},
                        {ICON_175,"la la-frown-o"},
                        //{ICON_176,"la la-futbol-o"},
                        //{ICON_177,"la la-gamepad"},
                        {ICON_178,"la la-gavel"},
                        {ICON_179,"la la-gear"},
                        {ICON_180,"la la-gears"},
                        //{ICON_181,"la la-genderless"},
                        //{ICON_182,"la la-gift"},
                        //{ICON_183,"la la-glass"},
                        //{ICON_184,"la la-globe"},
                        {ICON_185,"la la-graduation-cap"},
                        {ICON_186,"la la-group"},
                        //{ICON_187,"la la-hard-of-hearing"},
                        //{ICON_188,"la la-hdd-o"},
                        //{ICON_189,"la la-handshake-o"},
                        //{ICON_190,"la la-hashtag"},
                        //{ICON_191,"la la-headphones"},
                        //{ICON_192,"la la-heart"},
                        //{ICON_193,"la la-heart-o"},
                        //{ICON_194,"la la-heartbeat"},
                        {ICON_195,"la la-history"},
                        {ICON_196,"la la-home"},
                        //{ICON_197,"la la-hotel"},
                        {ICON_198,"la la-hourglass"},
                        {ICON_199,"la la-hourglass-1"},
                        {ICON_200,"la la-hourglass-2"},
                        //{ICON_201,"la la-hourglass-3"},
                        {ICON_202,"la la-hourglass-end"},
                        //{ICON_203,"la la-hourglass-half"},
                        //{ICON_204,"la la-hourglass-o"},
                        //{ICON_205,"la la-hourglass-start"},
                        //{ICON_206,"la la-i-cursor"},
                        //{ICON_207,"la la-id-badge"},
                        //{ICON_208,"la la-id-card"},
                        //{ICON_209,"la la-id-card-o"},
                        {ICON_210,"la la-image"},
                        //{ICON_211,"la la-inbox"},
                        {ICON_212,"la la-industry"},
                        {ICON_213,"la la-info"},
                        {ICON_214,"la la-info-circle"},
                        {ICON_215,"la la-institution"},
                        {ICON_216,"la la-key"},
                        //{ICON_217,"la la-keyboard-o"},
                        {ICON_218,"la la-language"},
                        //{ICON_219,"la la-laptop"},
                        //{ICON_220,"la la-leaf"},
                        //{ICON_221,"la la-legal"},
                        {ICON_222,"la la-lemon-o"},
                        {ICON_223,"la la-level-down"},
                        {ICON_224,"la la-level-up"},
                        //{ICON_225,"la la-life-bouy"},
                        //{ICON_226,"la la-life-buoy"},
                        //{ICON_227,"la la-life-ring"},
                        //{ICON_228,"la la-life-saver"},
                        {ICON_229,"la la-lightbulb-o"},
                        {ICON_230,"la la-line-chart"},
                        //{ICON_231,"la la-location-arrow"},
                        {ICON_232,"la la-lock"},
                        //{ICON_233,"la la-low-vision"},
                        //{ICON_234,"la la-magic"},
                        {ICON_235,"la la-magnet"},
                        {ICON_236,"la la-mail-forward"},
                        {ICON_237,"la la-mail-reply"},
                        {ICON_238,"la la-mail-reply-all"},
                        //{ICON_239,"la la-male"},
                        {ICON_240,"la la-map"},
                        //{ICON_241,"la la-map-o"},
                        //{ICON_242,"la la-map-pin"},
                        //{ICON_243,"la la-map-signs"},
                        //{ICON_244,"la la-map-marker"},
                        //{ICON_245,"la la-meh-o"},
                        //{ICON_246,"la la-microchip"},
                        //{ICON_247,"la la-microphone"},
                        //{ICON_248,"la la-microphone-slash"},
                        //{ICON_249,"la la-minus"},
                        //{ICON_250,"la la-minus-circle"},
                        //{ICON_251,"la la-minus-square"},
                        //{ICON_252,"la la-minus-square-o"},
                        //{ICON_253,"la la-mobile"},
                        //{ICON_254,"la la-mobile-phone"},
                        {ICON_255,"la la-money"},
                        //{ICON_256,"la la-moon-o"},
                        {ICON_257,"la la-mortar-board"},
                        //{ICON_258,"la la-motorcycle"},
                        //{ICON_259,"la la-mouse-pointer"},
                        //{ICON_260,"la la-music"},
                        //{ICON_261,"la la-navicon"},
                        //{ICON_262,"la la-newspaper-o"},
                        //{ICON_263,"la la-object-group"},
                        //{ICON_264,"la la-object-ungroup"},
                        //{ICON_265,"la la-paint-brush"},
                        //{ICON_266,"la la-paper-plane"},
                        //{ICON_267,"la la-paper-plane-o"},
                        //{ICON_268,"la la-paw"},
                        {ICON_269,"la la-pencil"},
                        {ICON_270,"la la-pencil-square"},
                        //{ICON_271,"la la-pencil-square-o"},
                        //{ICON_272,"la la-percent"},
                        {ICON_273,"la la-phone"},
                        //{ICON_274,"la la-phone-square"},
                        {ICON_275,"la la-photo"},
                        //{ICON_276,"la la-picture-o"},
                        {ICON_277,"la la-pie-chart"},
                        //{ICON_278,"la la-plane"},
                        //{ICON_279,"la la-plug"},
                        //{ICON_280,"la la-plus"},
                        //{ICON_281,"la la-plus-circle"},
                        //{ICON_282,"la la-plus-square"},
                        //{ICON_283,"la la-plus-square-o"},
                        //{ICON_284,"la la-podcast"},
                        //{ICON_285,"la la-power-off"},
                        //{ICON_286,"la la-print"},
                        //{ICON_287,"la la-puzzle-piece"},
                        //{ICON_288,"la la-qrcode"},
                        {ICON_289,"la la-question"},
                        {ICON_290,"la la-question-circle"},
                        //{ICON_291,"la la-question-circle-o"},
                        {ICON_292,"la la-quote-left"},
                        {ICON_293,"la la-quote-right"},
                        {ICON_294,"la la-random"},
                        {ICON_295,"la la-recycle"},
                        {ICON_296,"la la-refresh"},
                        {ICON_297,"la la-registered"},
                        {ICON_298,"la la-remove"},
                        //{ICON_299,"la la-reorder"},
                        //{ICON_300,"la la-reply"},
                        //{ICON_301,"la la-reply-all"},
                        //{ICON_302,"la la-retweet"},
                        //{ICON_303,"la la-road"},
                        //{ICON_304,"la la-rocket"},
                        {ICON_305,"la la-rss"},
                        //{ICON_306,"la la-rss-square"},
                        //{ICON_307,"la la-s15"},
                        {ICON_308,"la la-search"},
                        {ICON_309,"la la-search-minus"},
                        {ICON_310,"la la-search-plus"},
                        {ICON_311,"la la-send"},
                        //{ICON_312,"la la-send-o"},
                        //{ICON_313,"la la-server"},
                        //{ICON_314,"la la-share"},
                        //{ICON_315,"la la-share-alt"},
                        //{ICON_316,"la la-share-alt-square"},
                        //{ICON_317,"la la-share-square"},
                        //{ICON_318,"la la-share-square-o"},
                        {ICON_319,"la la-shield"},
                        //{ICON_320,"la la-ship"},
                        //{ICON_321,"la la-shopping-bag"},
                        //{ICON_322,"la la-shopping-basket"},
                        {ICON_323,"la la-shopping-cart"},
                        //{ICON_324,"la la-shower"},
                        {ICON_325,"la la-sign-in"},
                        {ICON_326,"la la-sign-out"},
                        //{ICON_327,"la la-sign-language"},
                        {ICON_328,"la la-signal"},
                        //{ICON_329,"la la-signing"},
                        //{ICON_330,"la la-sitemap"},
                        //{ICON_331,"la la-sliders"},
                        //{ICON_332,"la la-smile-o"},
                        //{ICON_333,"la la-snowflake-o"},
                        //{ICON_334,"la la-soccer-ball-o"},
                        //{ICON_335,"la la-sort"},
                        //{ICON_336,"la la-sort-alpha-asc"},
                        //{ICON_337,"la la-sort-alpha-desc"},
                        //{ICON_338,"la la-sort-amount-asc"},
                        //{ICON_339,"la la-sort-amount-desc	"},
                        //{ICON_340,"la la-sort-asc"},
                        //{ICON_341,"la la-sort-desc"},
                        //{ICON_342,"la la-sort-down"},
                        //{ICON_343,"la la-sort-numeric-asc"},
                        //{ICON_344,"la la-sort-numeric-desc"},
                        //{ICON_345,"la la-sort-up"},
                        //{ICON_346,"la la-space-shuttle"},
                        //{ICON_347,"la la-spinner"},
                        //{ICON_348,"la la-spoon"},
                        //{ICON_349,"la la-square"},
                        //{ICON_350,"la la-square-o"},
                        //{ICON_351,"la la-star"},
                        //{ICON_352,"la la-star-half"},
                        //{ICON_353,"la la-star-half-empty"},
                        //{ICON_354,"la la-star-half-full"},
                        //{ICON_355,"la la-star-half-o"},
                        //{ICON_356,"la la-star-o"},
                        {ICON_357,"la la-sticky-note"},
                        //{ICON_358,"la la-sticky-note-o"},
                        {ICON_359,"la la-street-view"},
                        {ICON_360,"la la-suitcase"},
                        //{ICON_361,"la la-sun-o"},
                        //{ICON_362,"la la-support"},
                        //{ICON_363,"la la-tablet"},
                        //{ICON_364,"la la-tachometer"},
                        {ICON_365,"la la-tag"},
                        {ICON_366,"la la-tags"},
                        {ICON_367,"la la-tasks"},
                        //{ICON_368,"la la-taxi"},
                        //{ICON_369,"la la-television"},
                        //{ICON_370,"la la-terminal"},
                        //{ICON_371,"la la-thermometer"},
                        //{ICON_372,"la la-thermometer-0"},
                        //{ICON_373,"la la-thermometer-1"},
                        //{ICON_374,"la la-thermometer-2"},
                        //{ICON_375,"la la-thermometer-3"},
                        //{ICON_376,"la la-thermometer-4"},
                        //{ICON_377,"la la-thermometer-empty"},
                        //{ICON_378,"la la-thermometer-full"},
                        //{ICON_379,"la la-thermometer-half"},
                        //{ICON_380,"la la-thermometer-quarter"},
                        //{ICON_381,"la la-thermometer-three-quarters"},
                        {ICON_382,"la la-thumb-tack"},
                        {ICON_383,"la la-thumbs-down"},
                        //{ICON_384,"la la-thumbs-o-up"},
                        {ICON_385,"la la-thumbs-up"},
                        //{ICON_386,"la la-ticket"},
                        {ICON_387,"la la-times"},
                        //{ICON_388,"la la-times-circle"},
                        //{ICON_389,"la la-times-circle-o"},
                        //{ICON_390,"la la-times-rectangle"},
                        //{ICON_391,"la la-times-rectangle-o"},
                        {ICON_392,"la la-tint"},
                        //{ICON_393,"la la-toggle-down"},
                        //{ICON_394,"la la-toggle-left"},
                        //{ICON_395,"la la-toggle-right"},
                        //{ICON_396,"la la-toggle-up"},
                        //{ICON_397,"la la-toggle-off"},
                        //{ICON_398,"la la-toggle-on"},
                        {ICON_399,"la la-trademark"},
                        {ICON_400,"la la-trash"},
                        //{ICON_401,"la la-trash-o"},
                        {ICON_402,"la la-tree"},
                        {ICON_403,"la la-trophy"},
                        //{ICON_404,"la la-truck"},
                        //{ICON_405,"la la-tty"},
                        //{ICON_406,"la la-tv"},
                        //{ICON_407,"la la-umbrella"},
                        //{ICON_408,"la la-universal-access"},
                        //{ICON_409,"la la-university"},
                        //{ICON_410,"la la-unlock"},
                        //{ICON_411,"la la-unlock-alt"},
                        //{ICON_412,"la la-unsorted"},
                        {ICON_413,"la la-upload"},
                        {ICON_414,"la la-user"},
                        //{ICON_415,"la la-user-circle"},
                        //{ICON_416,"la la-user-circle-o"},
                        //{ICON_417,"la la-user-o"},
                        //{ICON_418,"la la-user-plus"},
                        //{ICON_419,"la la-user-secret"},
                        //{ICON_420,"la la-user-times"},
                        {ICON_421,"la la-users"},
                        //{ICON_422,"la la-vcard"},
                        //{ICON_423,"la la-vcard-o"},
                        //{ICON_424,"la la-video-camera"},
                        //{ICON_425,"la la-volume-control-phone"},
                        //{ICON_426,"la la-volume-down"},
                        //{ICON_427,"la la-volume-off"},
                        //{ICON_428,"la la-volume-up"},
                        //{ICON_429,"la la-warning"},
                        //{ICON_430,"la la-wheelchair"},
                        //{ICON_431,"la la-wheelchair-alt"},
                        //{ICON_432,"la la-window-close"},
                        //{ICON_433,"la la-window-close-o"},
                        //{ICON_434,"la la-window-maximize"},
                        //{ICON_435,"la la-window-minimize"},
                        //{ICON_436,"la la-window-restore"},
                        {ICON_437,"la la-wifi"},
                        {ICON_438,"la la-wrench"},
                    };
            }
        }
        public static class SOCIAL_NETWORK
        {
            public class TYPES
            {
                public const byte FACEBOOK = 1;
                public const byte TWITTER = 2;
                public const byte INSTAGRAM = 3;
                public const byte YOUTUBE = 4;
                public const byte WHATSAPP = 5;
                public const byte LINKEDIN = 6;

                public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                    {
                        { FACEBOOK, "Facebook" },
                        { TWITTER, "Twitter" },
                        { INSTAGRAM, "Instagram" },
                        { YOUTUBE, "Youtube" },
                        { WHATSAPP, "Whatsapp" },
                        { LINKEDIN, "Linkedin" },
                    };
            }
        }
        public static class SELECT2
        {
            public static class DEFAULT
            {
                public const int PAGE_SIZE = 10;
            }

            public static class SERVER_SIDE
            {
                public static class REQUEST_PARAMETERS
                {
                    public const string CURRENT_PAGE = "page";
                    public const string QUERY = "q";
                    public const string REQUEST_TYPE = "_type";
                    public const string SEARCH_TERM = "term";
                }

                public static class REQUEST_TYPE
                {
                    public const string QUERY = "query";
                    public const string QUERY_APPEND = "query_append";
                }
            }
        }
        public static class DATATABLE
        {
            public static class SERVER_SIDE
            {
                public static class DEFAULT
                {
                    public const string ORDER_DIRECTION = "DESC";
                }

                public static class SENT_PARAMETERS
                {
                    public const string DRAW_COUNTER = "draw";
                    public const string PAGING_FIRST_RECORD = "start";
                    public const string RECORDS_PER_DRAW = "length";
                    public const string SEARCH_VALUE = "search[value]";
                    public const string SEARCH_REGEX = "search[regex]";
                    public const string ORDER_COLUMN = "order[0][column]";
                    public const string ORDER_DIRECTION = "order[0][dir]";
                }
            }
        }
        public static class PAGINATION
        {
            public static class SERVER_SIDE
            {
                public static class SENT_PARAMETERS
                {
                    public const string PAGE = "page";
                    public const string RECORDS_PER_DRAW_PAGINATION = "rpdraw";
                    public const string SEARCH_VALUE_PAGINATION = "srch";
                }
            }
        }
        public static class MONTHS
        {
            public const int JANUARY = 1;
            public const int FEBRAURY = 2;
            public const int MARCH = 3;
            public const int APRIL = 4;
            public const int MAY = 5;
            public const int JUNE = 6;
            public const int JULY = 7;
            public const int AUGUST = 8;
            public const int SEPTEMBER = 9;
            public const int OCTOBER = 10;
            public const int NOVEMBER = 11;
            public const int DECEMBER = 12;

            public static Dictionary<int, string> VALUES = new Dictionary<int, string>()
            {
                { JANUARY, "Enero" },
                { FEBRAURY, "Febrero" },
                { MARCH, "Marzo" },
                { APRIL, "Abril" },
                { MAY, "Mayo" },
                { JUNE, "Junio" },
                { JULY, "Julio" },
                { AUGUST, "Agosto" },
                { SEPTEMBER, "Setiembre" },
                { OCTOBER, "Octubre" },
                { NOVEMBER, "Noviembre" },
                { DECEMBER, "Diciembre" }
            };
        }
        public static class FORMATS
        {
            public const string DATE = "dd/MM/yyyy";
            public const string DURATION = "{0}h {1}m";
            public const string TIME = "h:mm tt";
            public const string DATETIME = "dd/MM/yyyy h:mm tt";
        }
        public static class TIMEZONEINFO
        {
            public const bool DisableDaylightSavingTime = true;
            public const int Gmt = -5;
            public const string LINUX_TIMEZONE_ID = "America/Bogota";
            public const string OSX_TIMEZONE_ID = "America/Cayman";
            public const string WINDOWS_TIMEZONE_ID = "SA Pacific Standard Time";
        }
        public static class HTML
        {
            public static class COLOR
            {
                public const string BRAND = "brand";
                public const string METAL = "metal";
                public const string PRIMARY = "primary";
                public const string SUCCESS = "success";
                public const string INFO = "info";
                public const string WARNING = "warning";
                public const string DANGER = "danger";
                public const string FOCUS = "focus";
                public const string ACCENT = "accent";

                private static Dictionary<int, string> INDICES = new Dictionary<int, string>()
                {
                    { 1, BRAND },
                    { 2, METAL },
                    { 3, PRIMARY},
                    { 4, SUCCESS },
                    { 5, INFO },
                    { 6, WARNING },
                    { 7, DANGER },
                    { 8, FOCUS }
                };

                public static string RANDOM_COLOR()
                {
                    var random = new System.Random();
                    var number = random.Next(1, 8);
                    return INDICES[number];
                }

            }
        }
        public class SEQUENCE_ORDER
        {
            public const byte NINGUNO = 0;
            public const byte FIRST = 1;
            public const byte SECOND = 2;
            public const byte THIRD = 3;
            public const byte FOURTH = 4;
            public const byte FIFTH = 5;
            public const byte SIXTH = 6;
            public const byte SEVENTH = 7;
            public const byte EIGHTH = 8;
            public const byte NINETH = 9;
            public const byte TENTH = 10;

            public static Dictionary<int, string> VALUES = new Dictionary<int, string>
                {
                    { NINGUNO, "Sin Asignar" },
                    { FIRST, "PRIMERO" },
                    { SECOND, "SEGUNDO" },
                    { THIRD, "TERCERO" },
                    { FOURTH, "CUARTO" },
                    { FIFTH, "QUINTO" },
                    { SIXTH, "SEXTO" },
                    { SEVENTH, "SÉPTIMO" },
                    { EIGHTH, "OCTAVO" },
                    { NINETH, "NOVENO" },
                    { TENTH, "DÉCIMO" },
                };
        }
        public class DOCUMENT_TYPE
        {
            public const byte DNI = 1;
            public const byte PASSPORT = 2;
            public const byte INMIGRATION_CARD = 3;
            public const byte TEMPORARY_RESIDENCE_CARD = 4;
            public const byte IDENTITY_CARD = 5;

            public static Dictionary<byte, string> VALUES = new Dictionary<byte, string>
                {
                    { DNI, "D.N.I." },
                    { PASSPORT, "Pasaporte" },
                    { INMIGRATION_CARD, "Carné  de extranjería" },
                    { TEMPORARY_RESIDENCE_CARD, "Carné temporal de permanencia" },
                    { IDENTITY_CARD, "Doc. identificación personal" },
                };
        }
        public static class EMAIL_ORGANITATION
        {
            public const string ADVISER = "asesor@legalconnection.pe";
            public const string SUPPORTTECHNICAL = "soporte@legalconnection.pe";
        }

        public static class WEEKDAY
        {
            public static string GetWeekDayName(string dayOfWeek)
            {
                if (dayOfWeek == DayOfWeek.Monday.ToString())
                    return "Lunes";
                if (dayOfWeek == DayOfWeek.Tuesday.ToString())
                    return "Martes";
                if (dayOfWeek == DayOfWeek.Wednesday.ToString())
                    return "Míercoles";
                if (dayOfWeek == DayOfWeek.Thursday.ToString())
                    return "Jueves";
                if (dayOfWeek == DayOfWeek.Friday.ToString())
                    return "Viernes";
                if (dayOfWeek == DayOfWeek.Saturday.ToString())
                    return "Sábado";
                if (dayOfWeek == DayOfWeek.Sunday.ToString())
                    return "Domingo";

                return string.Empty;
            }
        }
        public class CLOUD_CONTAINERS
        {
            public const string PORTAL = "portal";
            public const string MISSION_VISION = "misionvision";
            public const string PROFILE = "fotosperfil";
            public const string LEGAL_CASE_FILE = "casoslegales";
            public const string LAWYER_PUBLICATIONS = "abogadopublicaciones";
            public const string LAWYER_EXPERIENCES = "abogadoexperiencias";
            public const string WITHDRAWAL_REQUEST = "solicitudesretiro";
            public const string DEPOSIT_VOUCHERS = "comprobantesdeposito";
        }
    }
}
