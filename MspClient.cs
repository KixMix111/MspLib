using FluorineFx.AMF3;
using MspLib.Autorization;
using MspLib.Class;
using MspLib.Socket;
using MspLib.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MspLib
{
    public class MspClient
    {
        private MspConfig Config = null;
        private LoginResult loginResult = null;
        public MspSocketUser mspSocketUser = null;
        public MspClient(MspConfig Config)
        {
            this.Config = Config;
            if(Config.UseSocket)
            {
                mspSocketUser = new MspSocketUser(MspClientHelper.ParseServer(Config.Server));
            }
        }
        public async Task<LoginResult> LoginAsync(string Username, string Password)
        {
            LoginResult loginResult = new LoginResult();
            MspResponse loginResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.WebService.User.AMFUserServiceWeb.Login", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                Username,
                Password,
                Array.Empty<object>(),
                null,
                null,
                "MSP1-Standalone:XXXXXX"
            });
            if (loginResponse.Success)
            {
                ActorDetails actorDetails = new ActorDetails();
                PiggyBank piggyBank = new PiggyBank();
                NebulaLoginStatus nebulaLoginStatus = new NebulaLoginStatus();
                switch ((string)loginResponse.Response["loginStatus"]["status"])
                {
                    case "ThirdPartyCreated":
                        {
                            loginResult.Status = (string)loginResponse.Response["loginStatus"]["status"];
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            actorDetails.ActorId = loginResponse.Response["loginStatus"]["actor"]["ActorId"];
                            actorDetails.Name = loginResponse.Response["loginStatus"]["actor"]["Name"];
                            actorDetails.Level = loginResponse.Response["loginStatus"]["actor"]["Level"];
                            actorDetails.IsMale = (loginResponse.Response["loginStatus"]["actor"]["SkinSWF"] == "maleskin") ? true : false;
                            actorDetails.SkinColor = loginResponse.Response["loginStatus"]["actor"]["SkinColor"];
                            actorDetails.NoseId = loginResponse.Response["loginStatus"]["actor"]["NoseId"];
                            actorDetails.EyeId = loginResponse.Response["loginStatus"]["actor"]["EyeId"];
                            actorDetails.MouthId = loginResponse.Response["loginStatus"]["actor"]["MouthId"];
                            actorDetails.Money = loginResponse.Response["loginStatus"]["actor"]["Money"];
                            actorDetails.EyeColors = loginResponse.Response["loginStatus"]["actor"]["EyeColors"];
                            actorDetails.MouthColors = loginResponse.Response["loginStatus"]["actor"]["MouthColors"];
                            actorDetails.Fame = loginResponse.Response["loginStatus"]["actor"]["Fame"];
                            actorDetails.Fortune = loginResponse.Response["loginStatus"]["actor"]["Fortune"];
                            actorDetails.FriendCount = loginResponse.Response["loginStatus"]["actor"]["FriendCount"];
                            actorDetails.Created = loginResponse.Response["loginStatus"]["actor"]["Created"];
                            actorDetails.LastLogin = loginResponse.Response["loginStatus"]["actor"]["LastLogin"];
                            actorDetails.IsModerator = (loginResponse.Response["loginStatus"]["actor"]["Moderator"] > 0) ? true : false;
                            actorDetails.ProfileDisplays = loginResponse.Response["loginStatus"]["actor"]["ProfileDisplays"];
                            actorDetails.IsExtra = (loginResponse.Response["loginStatus"]["actor"]["IsExtra"] > 0) ? true : false;
                            actorDetails.InvitedByActorId = loginResponse.Response["loginStatus"]["actor"]["InvitedByActorId"];
                            actorDetails.NumberOfGiftsGiven = loginResponse.Response["loginStatus"]["actor"]["NumberOfGiftsGiven"];
                            actorDetails.NumberOfGiftsReceived = loginResponse.Response["loginStatus"]["actor"]["NumberOfGiftsReceived"];
                            actorDetails.NumberOfAutographsReceived = loginResponse.Response["loginStatus"]["actor"]["NumberOfAutographsReceived"];
                            actorDetails.NumberOfAutographsGiven = loginResponse.Response["loginStatus"]["actor"]["NumberOfAutographsGiven"];
                            actorDetails.TimeOfLastAutographGiven = loginResponse.Response["loginStatus"]["actor"]["TimeOfLastAutographGiven"];
                            actorDetails.MembershipPurchasedDate = loginResponse.Response["loginStatus"]["actor"]["MembershipPurchasedDate"];
                            actorDetails.MembershipTimeoutDate = loginResponse.Response["loginStatus"]["actor"]["MembershipTimeoutDate"];
                            actorDetails.LockedUntil = loginResponse.Response["loginStatus"]["actor"]["LockedUntil"];
                            actorDetails.LockedText = loginResponse.Response["loginStatus"]["actor"]["LockedText"];
                            actorDetails.FriendCountVIP = loginResponse.Response["loginStatus"]["actor"]["FriendCountVIP"];
                            actorDetails.Diamonds = loginResponse.Response["loginStatus"]["actor"]["Diamonds"];
                            actorDetails.Email = loginResponse.Response["loginStatus"]["actor"]["Email"];
                            loginResult.Actor = actorDetails;
                            piggyBank.StarCoins = loginResponse.Response["loginStatus"]["piggyBank"]["StarCoins"];
                            piggyBank.Fame = loginResponse.Response["loginStatus"]["piggyBank"]["Fame"];
                            piggyBank.Diamonds = loginResponse.Response["loginStatus"]["piggyBank"]["Diamonds"];
                            loginResult.PiggyBank = piggyBank;
                            loginResult.Ticket = loginResponse.Response["loginStatus"]["ticket"];
                            nebulaLoginStatus.AccessToken = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["accessToken"];
                            nebulaLoginStatus.RefreshToken = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["refreshToken"];
                            nebulaLoginStatus.ExpiresDate = DateTime.Now.AddSeconds(loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["expiresDate"]);
                            nebulaLoginStatus.ProfileId = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["profileId"];
                            if (Config.UseSocket)
                            {
                                mspSocketUser.SendAuthentification(MspClientHelper.ParseServer(Config.Server), loginResult.NebulaLoginStatus.AccessToken, loginResult.NebulaLoginStatus.ProfileId);
                            }
                            break;
                        }
                    case "Success":
                        {
                            loginResult.Status = (string)loginResponse.Response["loginStatus"]["status"];
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            actorDetails.ActorId = loginResponse.Response["loginStatus"]["actor"]["ActorId"];
                            actorDetails.Name = loginResponse.Response["loginStatus"]["actor"]["Name"];
                            actorDetails.Level = loginResponse.Response["loginStatus"]["actor"]["Level"];
                            actorDetails.IsMale = (loginResponse.Response["loginStatus"]["actor"]["SkinSWF"] == "maleskin") ? true : false;
                            actorDetails.SkinColor = loginResponse.Response["loginStatus"]["actor"]["SkinColor"];
                            actorDetails.NoseId = loginResponse.Response["loginStatus"]["actor"]["NoseId"];
                            actorDetails.EyeId = loginResponse.Response["loginStatus"]["actor"]["EyeId"];
                            actorDetails.MouthId = loginResponse.Response["loginStatus"]["actor"]["MouthId"];
                            actorDetails.Money = loginResponse.Response["loginStatus"]["actor"]["Money"];
                            actorDetails.EyeColors = loginResponse.Response["loginStatus"]["actor"]["EyeColors"];
                            actorDetails.MouthColors = loginResponse.Response["loginStatus"]["actor"]["MouthColors"];
                            actorDetails.Fame = loginResponse.Response["loginStatus"]["actor"]["Fame"];
                            actorDetails.Fortune = loginResponse.Response["loginStatus"]["actor"]["Fortune"];
                            actorDetails.FriendCount = loginResponse.Response["loginStatus"]["actor"]["FriendCount"];
                            actorDetails.Created = loginResponse.Response["loginStatus"]["actor"]["Created"];
                            actorDetails.LastLogin = loginResponse.Response["loginStatus"]["actor"]["LastLogin"];
                            actorDetails.IsModerator = (loginResponse.Response["loginStatus"]["actor"]["Moderator"] > 0) ? true : false;
                            actorDetails.ProfileDisplays = loginResponse.Response["loginStatus"]["actor"]["ProfileDisplays"];
                            actorDetails.IsExtra = (loginResponse.Response["loginStatus"]["actor"]["IsExtra"] > 0) ? true : false;
                            actorDetails.InvitedByActorId = loginResponse.Response["loginStatus"]["actor"]["InvitedByActorId"];
                            actorDetails.NumberOfGiftsGiven = loginResponse.Response["loginStatus"]["actor"]["NumberOfGiftsGiven"];
                            actorDetails.NumberOfGiftsReceived = loginResponse.Response["loginStatus"]["actor"]["NumberOfGiftsReceived"];
                            actorDetails.NumberOfAutographsReceived = loginResponse.Response["loginStatus"]["actor"]["NumberOfAutographsReceived"];
                            actorDetails.NumberOfAutographsGiven = loginResponse.Response["loginStatus"]["actor"]["NumberOfAutographsGiven"];
                            actorDetails.TimeOfLastAutographGiven = loginResponse.Response["loginStatus"]["actor"]["TimeOfLastAutographGiven"];
                            actorDetails.MembershipPurchasedDate = loginResponse.Response["loginStatus"]["actor"]["MembershipPurchasedDate"];
                            actorDetails.MembershipTimeoutDate = loginResponse.Response["loginStatus"]["actor"]["MembershipTimeoutDate"];
                            actorDetails.LockedUntil = loginResponse.Response["loginStatus"]["actor"]["LockedUntil"];
                            actorDetails.LockedText = loginResponse.Response["loginStatus"]["actor"]["LockedText"];
                            actorDetails.FriendCountVIP = loginResponse.Response["loginStatus"]["actor"]["FriendCountVIP"];
                            actorDetails.Diamonds = loginResponse.Response["loginStatus"]["actor"]["Diamonds"];
                            actorDetails.Email = loginResponse.Response["loginStatus"]["actor"]["Email"];
                            loginResult.Actor = actorDetails;
                            piggyBank.StarCoins = loginResponse.Response["loginStatus"]["piggyBank"]["StarCoins"];
                            piggyBank.Fame = loginResponse.Response["loginStatus"]["piggyBank"]["Fame"];
                            piggyBank.Diamonds = loginResponse.Response["loginStatus"]["piggyBank"]["Diamonds"];
                            loginResult.PiggyBank = piggyBank;
                            loginResult.Ticket = loginResponse.Response["loginStatus"]["ticket"];
                            nebulaLoginStatus.AccessToken = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["accessToken"];
                            nebulaLoginStatus.RefreshToken = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["refreshToken"];
                            nebulaLoginStatus.ExpiresDate = DateTime.Now.AddSeconds(loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["expiresIn"]);
                            nebulaLoginStatus.ProfileId = loginResponse.Response["loginStatus"]["nebulaLoginStatus"]["profileId"];
                            loginResult.NebulaLoginStatus = nebulaLoginStatus;
                            if (Config.UseSocket)
                            {
                                mspSocketUser.SendAuthentification(MspClientHelper.ParseServer(Config.Server), loginResult.NebulaLoginStatus.AccessToken, loginResult.NebulaLoginStatus.ProfileId);
                            }
                            break;
                        }
                    case "InvalidCredentials":
                        {
                            loginResult.Status = "Invalid username or password!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            break;
                        }
                    case "Blocked":
                        {
                            loginResult.Status = "Your ip is banned!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            if(loginResult.StatusDetails != null)
                            {
                                if(loginResult.StatusDetails.ToUpper() == "LOCKSOFT")
                                {
                                    loginResult.Status = "You are permanently locked out due to a VIP refund!";
                                    loginResult.StatusDetails = "42";
                                }
                            }
                            break;
                        }
                    case "WhiteListBlocked":
                        {
                            loginResult.Status = "No Access!";
                            loginResult.StatusDetails = loginResponse.Response["loginResult"]["statusDetails"];
                            break;
                        }
                    case "LockedUntil":
                        {
                            loginResult.Status = "Locked";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            if (loginResponse.Response["loginStatus"]["actor"]["IsExtra"] > 0)
                            {
                                loginResult.Status = "You are permanently locked!";
                            }
                            else
                            {
                                loginResult.Status = "You are temporary locked!";
                                actorDetails.LockedUntil = loginResponse.Response["loginStatus"]["actor"]["LockedUntil"];
                                actorDetails.LockedText = loginResponse.Response["loginStatus"]["actor"]["LockedText"];
                                loginResult.Actor = actorDetails;
                            }
                            break;
                        }
                    case "ForceEntityChange":
                        {
                            loginResult.Status = "You are forced to change your username!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            loginResult.Ticket = loginResponse.Response["loginStatus"]["ticket"];
                            break;
                        }
                    case "ERROR":
                        {
                            loginResult.Status = "You are rate limited!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            break;
                        }
                    case "SiteMaintenance":
                        {
                            loginResult.Status = "Msp API is currently in maintenance!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            break;
                        }
                    case "logintimeout":
                        {
                            loginResult.Status = "Msp API took too long to respond!";
                            loginResult.StatusDetails = loginResponse.Response["loginStatus"]["statusDetails"];
                            break;
                        }
                    default:
                        {
                            throw new Exception("Unexpected Login status: " + (string)loginResponse.Response["loginStatus"]["status"]);
                        }
                }
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + loginResponse.Status + " Error!");
            }
            this.loginResult = loginResult;
            return loginResult;
        }
        public async Task<NewForcedEntityNameResult> NewForcedEntityNameAsync(string NewUsername)
        {
            NewForcedEntityNameResult newForcedEntityNameResult = new NewForcedEntityNameResult();
            try
            {
                var loc12 = loginResult.StatusDetails.Split(',')[0];
                var ActorId = Convert.ToInt32(loc12.Split('|')[0]);
                var EntityId = Convert.ToInt32(loc12.Split('|')[1]);
                var EntityType = Convert.ToInt32(loc12.Split('|')[2]);
                var Name = loc12.Split('|')[3];
                MspResponse nameChangeResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.WebService.Admin.AMFAdminService.NewForcedEntityName", MspClientHelper.ParseServer(Config.Server), new object[]
                {
                    new TicketHeader()
                    {
                        Ticket = TicketGenerator.headerTicket(loginResult.Ticket)
                    },
                    (long)ActorId,
                    (long)EntityId,
                    (long)EntityType,
                    NewUsername
                });
                if (nameChangeResponse.Success)
                {
                    switch ((int)nameChangeResponse.Response["Code"])
                    {
                        case 0:
                            {
                                newForcedEntityNameResult.IsSuccess = true;
                                newForcedEntityNameResult.Status = "Success";
                                break;
                            }
                        case 1:
                            {
                                newForcedEntityNameResult.IsSuccess = false;
                                newForcedEntityNameResult.Status = "The chosen username is not allowed!";
                                break;
                            }
                        case 2:
                            {
                                newForcedEntityNameResult.IsSuccess = false;
                                newForcedEntityNameResult.Status = "The chosen username is already taken!";
                                break;
                            }
                        default:
                            {
                                newForcedEntityNameResult.IsSuccess = false;
                                newForcedEntityNameResult.Status = "Error!";
                                break;
                            }
                    }
                }
                else
                {
                    throw new Exception("MovieStarPlanet Server Return " + nameChangeResponse.Status + " Error!");
                }
            }
            catch
            {
                newForcedEntityNameResult.IsSuccess = false;
                newForcedEntityNameResult.Status = "You are not allowed to change your username!";
            }
            return newForcedEntityNameResult;
        }
        public async Task<ThirdPartySaveAvatarResult> ThirdPartySaveAvatarAsync(ThirdPartySaveAvatarData ThirdPartySaveAvatarData, byte[] SnapshotSmall, byte[] SnapshotBig)
        {
            MspResponse thirdPartySaveAvatarResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.WebService.AMFActorService.ThirdPartySaveAvatar", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                ThirdPartySaveAvatarData,
                SnapshotSmall,
                SnapshotBig,
                ThirdPartySaveAvatarData.ChosenActorName,
                ThirdPartySaveAvatarData.ChosenPassword
            });
            ThirdPartySaveAvatarResult thirdPartySaveAvatarResult = new ThirdPartySaveAvatarResult();
            if (thirdPartySaveAvatarResponse.Success)
            {
                if(thirdPartySaveAvatarResponse.Response.ToString() == "SUCCESS")
                {
                    thirdPartySaveAvatarResult.IsSuccess = true;
                }
                else
                {
                    thirdPartySaveAvatarResult.IsSuccess = false;
                }
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + thirdPartySaveAvatarResponse.Status + " Error!");
            }
            return thirdPartySaveAvatarResult;
        }
        public async Task<GetFriendListResult> GetFriendListAsync()
        {
            GetFriendListResult getFriendListResult = new GetFriendListResult();
            List<FriendData> friendDatas = new List<FriendData>();
            MspResponse getFriendListWithNameAndScoreResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.WebService.Friendships.AMFFriendshipService.GetFriendListWithNameAndScore", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                new TicketHeader()
                {
                    Ticket = TicketGenerator.headerTicket(loginResult.Ticket)
                },
                (long)loginResult.Actor.ActorId
            });
            if (getFriendListWithNameAndScoreResponse.Success)
            {
                for(int i = 0; i < ((ArrayCollection)getFriendListWithNameAndScoreResponse.Response).Count; i++)
                {
                    friendDatas.Add(new FriendData()
                    {
                        ActorId = getFriendListWithNameAndScoreResponse.Response[i]["actorId"],
                        Name = getFriendListWithNameAndScoreResponse.Response[i]["name"],
                        Level = getFriendListWithNameAndScoreResponse.Response[i]["level"],
                        RecentlyLoggedIn = getFriendListWithNameAndScoreResponse.Response[i]["recentlyLoggedIn"],
                        MembershipTimeoutDate = getFriendListWithNameAndScoreResponse.Response[i]["membershipTimeoutDate"],
                        LastInteractionDate = getFriendListWithNameAndScoreResponse.Response[i]["lastInteractionDate"],
                        Money = getFriendListWithNameAndScoreResponse.Response[i]["money"],
                        Fame = getFriendListWithNameAndScoreResponse.Response[i]["fame"],
                        Fortune = getFriendListWithNameAndScoreResponse.Response[i]["fortune"],
                        FriendCount = getFriendListWithNameAndScoreResponse.Response[i]["friendCount"],
                        RoomLikes = getFriendListWithNameAndScoreResponse.Response[i]["roomLikes"],
                        MembershipPurchasedDate = getFriendListWithNameAndScoreResponse.Response[i]["membershipPurchasedDate"],
                        LastLogin = getFriendListWithNameAndScoreResponse.Response[i]["lastLogin"],
                        IsModerator = (getFriendListWithNameAndScoreResponse.Response[i]["moderator"] > 0) ? true: false,
                        NebulaProfileId = getFriendListWithNameAndScoreResponse.Response[i]["nebulaProfileId"]
                    });
                }
                getFriendListResult.Friends = friendDatas;
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + getFriendListWithNameAndScoreResponse.Status + " Error!");
            }
            return getFriendListResult;
        }
        public async Task<GetActorIdFromNameResult> GetActorIdFromNameAsync(string Username)
        {
            GetActorIdFromNameResult getActorIdFromNameResult = new GetActorIdFromNameResult();
            MspResponse getActorIdFromNameResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.WebService.UserSession.AMFUserSessionService.GetActorIdFromName", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                Username
            });
            if (getActorIdFromNameResponse.Success)
            {
                getActorIdFromNameResult.ActorId = (int)getActorIdFromNameResponse.Response;
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + getActorIdFromNameResponse.Status + " Error!");
            }
            return getActorIdFromNameResult;
        }
        public async Task<CreateMovieResult> CreateMovieAsync(string Title, bool IsPublicMovie, byte[] MovieData, byte[] MovieActorClothesData, object[] ActorIdOfTheActors, byte[] Snapshot)
        {
            CreateMovieResult createMovieResult = new CreateMovieResult();
            MspResponse createMovieResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.MobileServices.AMFMovieService.CreateMovieWithSnapshot", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                new TicketHeader()
                {
                    Ticket = TicketGenerator.headerTicket(loginResult.Ticket)
                },
                Title,
                IsPublicMovie,
                new Random().Next(29999, 217330),
                MovieData,
                MovieActorClothesData,
                ActorIdOfTheActors,
                Snapshot,
                Snapshot
            });
            if (createMovieResponse.Success)
            {
                if(createMovieResponse.Response["movieId"] == -1)
                {
                    createMovieResult.IsSuccess = false;
                }
                else
                {
                    createMovieResult.IsSuccess = true;
                }
                createMovieResult.MovieId = createMovieResponse.Response["movieId"];
                if (Config.UseSocket)
                {
                    if(IsPublicMovie)
                    {
                        mspSocketUser.CreateMovie(loginResult.Actor.Name, loginResult.Actor.ActorId, createMovieResult.MovieId);
                    }
                }
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + createMovieResponse.Status + " Error!");
            }
            return createMovieResult;
        }
        public async Task<WatchMovieResult> WatchMovieAsync(int MovieId, string MovieAuthorProfileId = null)
        {
            WatchMovieResult watchMovieResult = new WatchMovieResult();
            MspResponse watchMovieResponse = await Request.SendPacketToMspApiAsync("MovieStarPlanet.MobileServices.AMFMovieService.MovieWatched", MspClientHelper.ParseServer(Config.Server), new object[]
            {
                new TicketHeader()
                {
                    Ticket = TicketGenerator.headerTicket(loginResult.Ticket)
                },
                MovieId
            });
            if (watchMovieResponse.Success)
            {
                watchMovieResult.AwardedFame = watchMovieResponse.Response["awardedFame"];
                watchMovieResult.ReturnType = watchMovieResponse.Response["returnType"];
                if (watchMovieResponse.Response["awardedFame"] == 10 && watchMovieResponse.Response["returnType"] == 2)
                {
                    watchMovieResult.IsSuccess = false;
                }
                else
                {
                    watchMovieResult.IsSuccess = true;
                    if (Config.UseSocket)
                    {
                        mspSocketUser.WatchMovie(MovieAuthorProfileId, loginResult.Actor.ActorId, loginResult.Actor.Name, MovieId);
                    }
                }
            }
            else
            {
                throw new Exception("MovieStarPlanet Server Return " + watchMovieResponse.Status + " Error!");
            }
            return watchMovieResult;
        }
    }
}
