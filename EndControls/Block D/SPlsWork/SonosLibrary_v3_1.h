namespace SonosLibrary.SimplPlusInterfaces.Events;
        // class declarations
         class BusyEventArgs;
         class ClearBusyEventArgs;
     class BusyEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING status[];

        // class properties
    };

     class ClearBusyEventArgs 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING status[];

        // class properties
    };

namespace SonosProLibrary.DataStructures;
        // class declarations
         class PlaybackStatusMessageStructure;
         class PlayModes;
         class AvailablePlaybackActions;
         class PlaybackStatusRootObject;
         class GroupCoordinatorChangeMessageStructure;
         class GroupCoordinatorChangeMessageRootObject;
         class FavoritesListChangeMessageStructure;
         class FavoriteListChangeMessageRootObject;
         class FavoriteListMessageStructure;
         class FavoriteListMessageRootObject;
         class ListItem;
         class SonosService;
         class GroupChangeMessageStructure;
         class GroupChangeMessageRootObject;
         class SonosApiGroup;
         class SonosApiPlayer;
         class PlaybackMetadataMessageStructure;
         class Container;
         class Item;
         class Track;
         class Album;
         class Artist;
         class Book;
         class Show;
         class ItemPolicies;
         class MusicObjectId;
         class Service;
         class MetadataRootObject;
     class PlaybackStatusMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class PlayModes 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class AvailablePlaybackActions 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class PlaybackStatusRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING playbackState[];
        SIGNED_LONG_INTEGER positionMillis;
        STRING queueVersion[];
        STRING itemId[];
        PlayModes playModes;
        AvailablePlaybackActions availablePlaybackActions;
        STRING previousItemId[];
        STRING previousPositionMillis[];
    };

     class GroupCoordinatorChangeMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class GroupCoordinatorChangeMessageRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING groupStatus[];
        STRING groupName[];
        STRING websocketUrl[];
        STRING playerId[];
    };

     class FavoritesListChangeMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class FavoriteListChangeMessageRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING version[];
    };

     class FavoriteListMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class FavoriteListMessageRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING version[];
        ListItem items[];
    };

     class ListItem 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING id[];
        STRING name[];
        STRING description[];
        STRING imageUrl[];
        SonosService service;
    };

     class SonosService 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        STRING id[];
        STRING imageUrl[];
    };

     class GroupChangeMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class GroupChangeMessageRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SonosApiGroup groups[];
        SonosApiPlayer players[];
    };

     class SonosApiGroup 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING coordinatorId[];
        STRING id[];
        STRING playbackState[];
        STRING playerIds[][];
        STRING name[];
    };

     class SonosApiPlayer 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING apiVersion[];
        STRING deviceIds[][];
        STRING icon[];
        STRING id[];
        STRING minApiVersion[];
        STRING name[];
        STRING restUrl[];
        STRING softwareVersion[];
        STRING webSocketUrl[];
    };

     class PlaybackMetadataMessageStructure 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class Container 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        STRING type[];
        MusicObjectId id;
        Service service;
        STRING imageUrl[];
        STRING tags[][];
    };

     class Item 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING id[];
        Track track;
        ItemPolicies policies;
    };

     class Track 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER durationMillis;
        MusicObjectId id;
        STRING imageUrl[];
        STRING name[];
        STRING tags[][];
        STRING type[];
        Service service;
        SIGNED_LONG_INTEGER trackNumber;
        Artist artist;
        Album album;
        SIGNED_LONG_INTEGER chapterNumber;
        Artist author;
        Artist narrator;
        Book book;
        Artist host;
        Show show;
    };

     class Album 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        Artist artist;
        STRING imageUrl[];
        MusicObjectId id;
    };

     class Artist 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        STRING imageUrl[];
        MusicObjectId id;
    };

     class Book 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        Artist author;
        STRING imageUrl[];
        MusicObjectId id;
    };

     class Show 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        Artist host;
        STRING imageUrl[];
        MusicObjectId id;
    };

     class ItemPolicies 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
    };

     class MusicObjectId 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING serviceId[];
        STRING objectId[];
        STRING accountId[];
    };

     class Service 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        STRING name[];
        STRING id[];
        STRING imageUrl[];
    };

     class MetadataRootObject 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        Container container;
        Item currentItem;
        Item nextItem;
        Show currentShow;
        STRING streamInfo[];
    };

namespace SonosProLibrary.API.Events;
        // class declarations
         class SonosApiInformationChangedEventArgs;
         class DeviceConfigurationCompleteEventArgs;
         class PlayerVolumeChangeEventArgs;
         class GroupVolumeChangeEventArgs;
         class PlaybackMetaDataChangeEventArgs;
         class PlaybackStatusChangeEventArgs;
         class FavoriteLoadedEventArgs;
         class GlobalErrorEventArgs;
         class OfflineStateChangedEventArgs;
     class SonosApiInformationChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class DeviceConfigurationCompleteEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class PlayerVolumeChangeEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class GroupVolumeChangeEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class PlaybackMetaDataChangeEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class PlaybackStatusChangeEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class FavoriteLoadedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class GlobalErrorEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Error[];
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

     class OfflineStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosApiEventType;

        // class properties
    };

namespace SonosLibrary.SimplPlusInterfaces.Impl;
        // class declarations
         class SystemSimplPlusInterface;
         class ZoneMenuSimplPlusInterface;
         class VolumeManagerSimplPlusInterface;
         class SonosSimplMediaPlayerMenu;
         class MediaPlayerDeviceSimplPlusInterface;
         class GroupingSimplPlusInterface;
           class DelegateFn;
           class DelegateFnUshort;
           class DelegateFnString;
     class SystemSimplPlusInterface 
    {
        // class delegates
        delegate FUNCTION DelegateFn ( );

        // class events

        // class functions
        FUNCTION Initialize ();
        FUNCTION DiscoverGroups ();
        FUNCTION StopDiscovery ();
        FUNCTION GetFavorites ();
        FUNCTION EnableLogger ();
        FUNCTION DisableLogger ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER BusyStatus;
        INTEGER DiscoveringGroups;
        INTEGER GettingFavorites;

        // class properties
        DelegateProperty DelegateFn PlayerBusyUpdate;
    };

     class ZoneMenuSimplPlusInterface 
    {
        // class delegates
        delegate FUNCTION DelegateFn ( );

        // class events

        // class functions
        FUNCTION RefreshZoneList ();
        FUNCTION SelectZone ( SIGNED_LONG_INTEGER index );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER BusyStatus;
        INTEGER DiscoveringGroups;
        STRING ListOfZoneNames[][];
        INTEGER NumberOfZoneItems;
        INTEGER SelectedZoneIndex;
        STRING SelectedZoneRoomName[];

        // class properties
        DelegateProperty DelegateFn BusyUpdate;
        DelegateProperty DelegateFn ZoneListUpdate;
        DelegateProperty DelegateFn ZoneListSelectionUpdate;
        INTEGER UseDeviceNames;
    };

     class VolumeManagerSimplPlusInterface 
    {
        // class delegates
        delegate FUNCTION DelegateFn ( );
        delegate FUNCTION DelegateFnUshort ( INTEGER value );

        // class events

        // class functions
        FUNCTION SetGroupVolume ( INTEGER volume );
        FUNCTION SetGroupMuteState ( INTEGER state );
        FUNCTION ToggleGroupMuteState ();
        FUNCTION RefreshPlayerList ();
        FUNCTION PlayerVolumeUpByPlayerListIndex ( INTEGER index );
        FUNCTION PlayerVolumeDownByPlayerListIndex ( INTEGER index );
        FUNCTION SetPlayerVolumeByPlayerListIndex ( INTEGER index , INTEGER volume );
        FUNCTION SetPlayerMuteStateByPlayerListIndex ( INTEGER index , INTEGER state );
        FUNCTION TogglePlayerMuteStateByPlayerListIndex ( INTEGER index );
        FUNCTION Initialize ( STRING name );
        FUNCTION SetDefaultPlayerName ( STRING name );
        FUNCTION GroupVolumeUp ();
        FUNCTION GroupVolumeDown ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER BusyStatus;
        INTEGER DiscoveringGroups;
        INTEGER GettingFavorites;
        STRING DefaultPlayerName[];
        STRING GroupName[];
        INTEGER IsGrouped;
        INTEGER IsPlaying;
        STRING PlaybackState[];
        INTEGER GroupIsMuted;
        INTEGER GroupVolumeIsFixed;
        INTEGER GroupVolumeLevel;
        INTEGER GroupVolumeChangeInProgress;
        INTEGER SimulatedGroupVolumeLevel;
        STRING ListOfPlayerNames[][];
        INTEGER ListOfPlayerVolumeIsFixedFeedback[];
        INTEGER ListOfPlayerVolumeFeedback[];
        INTEGER ListOfPlayerVolumeChangeInProgressFeedback[];
        INTEGER ListOfSimulatedPlayerVolumeFeedback[];
        INTEGER ListOfPlayerMuteFeedback[];
        INTEGER NumberOfPlayers;

        // class properties
        DelegateProperty DelegateFn IniatializationComplete;
        DelegateProperty DelegateFn PlayerBusyUpdate;
        DelegateProperty DelegateFn GroupInfoUpdate;
        DelegateProperty DelegateFn GroupVolumeUpdate;
        DelegateProperty DelegateFn GroupMuteStateUpdate;
        DelegateProperty DelegateFn GroupPlaybackStateUpdate;
        DelegateProperty DelegateFn PlayerListUpdate;
        DelegateProperty DelegateFnUshort PlayerListItemUpdate;
        DelegateProperty DelegateFnUshort PlayerListItemVolumeUpdate;
        DelegateProperty DelegateFnUshort PlayerListItemMuteStateUpdate;
    };

     class SonosSimplMediaPlayerMenu 
    {
        // class delegates

        // class events

        // class functions
        FUNCTION SetButtonCount ( SIGNED_LONG_INTEGER c );
        FUNCTION SetStatusMsgItems ( SIGNED_LONG_INTEGER i , STRING s );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        INTEGER __class_id__;

        // class properties
        SIGNED_LONG_INTEGER Version;
        SIGNED_LONG_INTEGER Instance;
        STRING Language[];
        STRING Title[];
        SIGNED_LONG_INTEGER ItemCnt;
        STRING FindDesired[];
        STRING FindSupported[][];
        SIGNED_LONG_INTEGER Level;
        STRING Sorted[];
        SIGNED_LONG_INTEGER MaxReqItems;
        STRING ListSpecificFunctions[][];
        STRING ActionsSupported[][];
        STRING ActionsAvailable[][];
        STRING Subtitle[];
    };

     class MediaPlayerDeviceSimplPlusInterface 
    {
        // class delegates
        delegate FUNCTION DelegateFn ( );
        delegate FUNCTION DelegateFnString ( SIMPLSHARPSTRING myString );

        // class events

        // class functions
        FUNCTION RefreshFavoritesList ();
        FUNCTION PlayFavoriteByName ( STRING name );
        FUNCTION PlayFavoriteWithDelay ( SIGNED_LONG_INTEGER favoriteselected );
        FUNCTION PlayFavorite ( STRING id );
        FUNCTION RequestStartPlayback ();
        FUNCTION RequestStopPlayback ();
        FUNCTION RequestTogglePlayPause ();
        FUNCTION NextTrack ();
        FUNCTION PreviousTrack ();
        FUNCTION Seek ( INTEGER time );
        FUNCTION SeekBar ( INTEGER time );
        FUNCTION SetRelativeSeekSeconds ( INTEGER time );
        FUNCTION SeekRelativeForward ();
        FUNCTION SeekRelativeBackwards ();
        FUNCTION ToggleRepeat ();
        FUNCTION SetRepeatMode ( INTEGER mode );
        FUNCTION ToggleShuffle ();
        FUNCTION SetShuffleMode ( INTEGER mode );
        FUNCTION PlayerVolumeUp ();
        FUNCTION PlayerVolumeDown ();
        FUNCTION SetPlayerVolumeLevel ( INTEGER volumelevel );
        FUNCTION SetVolumeLevelBar ( INTEGER volumelevel );
        FUNCTION MutePlayer ();
        FUNCTION UnmutePlayer ();
        FUNCTION ToggleMuteState ();
        FUNCTION GroupVolumeUp ();
        FUNCTION GroupVolumeDown ();
        FUNCTION SetGroupVolumeLevel ( INTEGER volumelevel );
        FUNCTION SetGroupVolumeLevelBar ( INTEGER volumelevel );
        FUNCTION MuteGroup ();
        FUNCTION UnmuteGroup ();
        FUNCTION ToggleGroupMuteState ();
        FUNCTION Initialize ( INTEGER port , INTEGER adapterId , SIGNED_LONG_INTEGER connectionType );
        FUNCTION MessageIn ( STRING pkt );
        FUNCTION SetDefaultPlayerName ( STRING name );
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING HouseholdId[];
        STRING WebsocketUrl[];
        STRING DeviceZoneName[];
        STRING DriverZoneName[];
        STRING ConfiguredZoneName[];
        STRING GroupName[];
        INTEGER IsGrouped;
        STRING ListOfFavorites[][];
        STRING ListOfFavoritesDescriptions[][];
        STRING ListOfFavoritesImageUrLs[][];
        INTEGER NumberOfFavorites;
        INTEGER SelectedFavorite;
        STRING FavoriteName[];
        STRING FavoriteDescription[];
        STRING FavoriteImageUrl[];
        INTEGER BusyStatus;
        INTEGER DiscoveringGroups;
        INTEGER GettingFavorites;
        INTEGER OfflineState;
        INTEGER IsPlaying;
        INTEGER IsPaused;
        INTEGER IsBuffering;
        INTEGER IsIdle;
        INTEGER NextTrackAvailable;
        INTEGER PreviousTrackAvailable;
        INTEGER ShowTrackProgress;
        INTEGER SeekingAvailable;
        INTEGER PauseAvailable;
        INTEGER StopAvailable;
        INTEGER RepeatAvailable;
        INTEGER RepeatOneAvailable;
        INTEGER ShuffleAvailable;
        INTEGER CrossfadeAvailable;
        INTEGER GroupIsMuted;
        INTEGER GroupVolumeIsFixed;
        INTEGER IsMuted;
        INTEGER VolumeIsFixed;
        INTEGER RepeatEnabled;
        INTEGER RepeatOneEnabled;
        INTEGER ShuffleEnabled;
        INTEGER CrossfadeEnabled;
        INTEGER GroupVolumeLevel;
        INTEGER Volume;
        INTEGER GroupVolumeLevelBar;
        INTEGER VolumeLevelBar;
        SIGNED_LONG_INTEGER NowPlayingTrackLengthMilliseconds;
        SIGNED_LONG_INTEGER NowPlayingTrackPositionMilliseconds;
        INTEGER NowPlayingTrackLengthSeconds;
        INTEGER NowPlayingTrackPositionSeconds;
        INTEGER NowPlayingTrackPositionGauge;
        STRING PlaybackState[];
        STRING PlaybackError[];
        INTEGER NowPlayingContainerAvailable;
        STRING NowPlayingContainerName[];
        STRING NowPlayingProviderName[];
        STRING NowPlayingProviderImageUrl[];
        STRING NowPlayingTrackName[];
        STRING NowPlayingArtist[];
        STRING NowPlayingAlbum[];
        STRING NowPlayingTrackNumber[];
        STRING NowPlayingStreamInfo[];
        STRING NowPlayingImageUrl[];
        STRING NowPlayingTrackLengthString[];
        STRING NowPlayingTrackPositionString[];
        STRING NowPlayingLines[][];

        // class properties
        DelegateProperty DelegateFn IniatializationComplete;
        DelegateProperty DelegateFn PlayerConnectionUpdate;
        DelegateProperty DelegateFn PlayerBusyUpdate;
        DelegateProperty DelegateFn BrowseListBusyUpdate;
        DelegateProperty DelegateFn FavoritesListUpdate;
        DelegateProperty DelegateFn PlayerInfoUpdate;
        DelegateProperty DelegateFn PlayerStateUpdate;
        DelegateProperty DelegateFn PlayerNowPlayingInfoUpdate;
        DelegateProperty DelegateFn PlayerNowPlayingProgressUpdate;
        DelegateProperty DelegateFn PlayerVolumeUpdate;
        DelegateProperty DelegateFn PlayerMuteUpdate;
        DelegateProperty DelegateFn GroupVolumeUpdate;
        DelegateProperty DelegateFn GroupMuteUpdate;
        DelegateProperty DelegateFn PlayerPlaybackErrorUpdate;
        STRING DefaultPlayerName[];
        STRING SonosDeviceKey[];
        STRING GroupId[];
        DelegateProperty DelegateFnString MessageOut;
    };

     class GroupingSimplPlusInterface 
    {
        // class delegates
        delegate FUNCTION DelegateFn ( );

        // class events

        // class functions
        FUNCTION GetGroupingInfo ();
        FUNCTION SetGroup ( SIGNED_LONG_INTEGER grouptoselect );
        FUNCTION UpdatePlayerSelectedFeedback ( SIGNED_LONG_INTEGER playertoupdate );
        FUNCTION AddOrRemovePlayers ();
        FUNCTION CreateGroupBySonosZoneNames ();
        FUNCTION ClearCreateGroupData ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING ListOfGroupNames[][];
        STRING ListOfNowPlayingInfoLine1[][];
        STRING ListOfNowPlayingInfoLine2[][];
        STRING ListOfNowPlayingInfoLine3[][];
        STRING ListOfNowPlayingInfoLine4[][];
        STRING ListOfNowPlayingInfoLine5[][];
        STRING ListOfNowPlayingImageUrl[][];
        INTEGER ListOfPlaybackStatus[];
        INTEGER NumberOfGroups;
        INTEGER NumberOfPlayersInGroup[];
        STRING ListOfPlayerNames[][];
        INTEGER ListOfSelectedFeedback[];
        INTEGER PreviousListOfSelectedFeedback[];
        INTEGER NumberOfPlayers;
        STRING NowPlayingContainerName[];
        STRING NowPlayingProviderName[];
        STRING NowPlayingProviderImageUrl[];
        STRING NowPlayingTrackName[];
        STRING NowPlayingStreamInfo[];
        STRING NowPlayingArtist[];
        STRING NowPlayingAlbum[];
        STRING NowPlayingImageUrl[];
        STRING NowPlayingLines[][];
        INTEGER BusyStatus;
        INTEGER DiscoveringGroups;
        INTEGER GettingFavorites;
        STRING CreateGroupMembers[][];
        STRING ContentSourceZoneName[];

        // class properties
        DelegateProperty DelegateFn PlayerBusyUpdate;
        DelegateProperty DelegateFn GroupsListUpdate;
        DelegateProperty DelegateFn GroupsNowPlayingUpdate;
        DelegateProperty DelegateFn GroupsPlaybackStatusUpdate;
        DelegateProperty DelegateFn PlayersListUpdate;
        DelegateProperty DelegateFn PlayersSelectedUpdate;
        DelegateProperty DelegateFn NowPlayingInfoUpdate;
    };

namespace SonosProLibrary.System.Events;
        // class declarations
         class SonosInformationChangedEventArgs;
         class SonosDiscoveryStartedEventArgs;
         class SonosDiscoveryCompletedEventArgs;
         class SonosConfigurationChangeStartedEventArgs;
         class SonosConfigurationChangeCompletedEventArgs;
         class SonosGettingFavoritesEventArgs;
         class SonosGettingFavoritesCompleteEventArgs;
         class SonosSystemOfflineStateEventArgs;
         class SonosSystemEnabledChangedEventArgs;
         class OnNewSonosDeviceDiscoveredEventArgs;
         class OnSonosDeviceUpdatedEventArgs;
         class OnSonosDeviceOfflineStateUpdatedEventArgs;
         class SonosDeviceDeletedEventArgs;
         class SonosPrimaryDeviceDriverChangedEventArgs;
         class SonosGroupModifiedEventArgs;
         class SonosGroupAddedEventArgs;
         class SonosGroupRemovedEventArgs;
         class SonosHouseholdIdChangedEventArgs;
         class SonosNumberOfDiscoveredHouseholdsChangedEventArgs;
     class SonosInformationChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosDiscoveryStartedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Mode;
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosDiscoveryCompletedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Mode;
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER Result;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosConfigurationChangeStartedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosConfigurationChangeCompletedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosGettingFavoritesEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosGettingFavoritesCompleteEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosSystemOfflineStateEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Mode;
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER Result;
        SIGNED_LONG_INTEGER SystemState;
        SIGNED_LONG_INTEGER SystemConnectionFailureReason;
        SIGNED_LONG_INTEGER SystemDeviceConnectionFailureReason;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosSystemEnabledChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnNewSonosDeviceDiscoveredEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDeviceUpdatedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDeviceOfflineStateUpdatedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosDeviceDeletedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosPrimaryDeviceDriverChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING PrimaryDeviceKey[];
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosGroupModifiedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosGroupAddedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosGroupRemovedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class SonosHouseholdIdChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
        STRING HouseholdId[];
    };

     class SonosNumberOfDiscoveredHouseholdsChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
        SIGNED_LONG_INTEGER NumberOfHouseholds;
    };

namespace SonosProLibrary.System.Enums;
        // class declarations
         class SonosSystemConstants;
         class SonosEventType;
    static class SonosSystemConstants 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        static SIGNED_LONG_INTEGER MaxNumberOfDevices;
        static SIGNED_LONG_INTEGER MaxNumberOfFavorites;
        static SIGNED_LONG_INTEGER MinVolumeLevel;
        static SIGNED_LONG_INTEGER MaxVolumeLevel;
        static SIGNED_LONG_INTEGER MaxSimplAnalogValue;

        // class properties
    };

    static class SonosEventType // enum
    {
        static SIGNED_LONG_INTEGER SonosDiscoveryStarted;
        static SIGNED_LONG_INTEGER SonosDiscoveryCompleted;
        static SIGNED_LONG_INTEGER SonosConfigurationChangeStarted;
        static SIGNED_LONG_INTEGER SonosConfigurationChangeCompleted;
        static SIGNED_LONG_INTEGER SonosGettingFavorites;
        static SIGNED_LONG_INTEGER SonosGettingFavoritesCompleted;
        static SIGNED_LONG_INTEGER SonosSystemOfflineState;
        static SIGNED_LONG_INTEGER SonosSystemEnabledChanged;
        static SIGNED_LONG_INTEGER OnNewSonosDeviceDiscovered;
        static SIGNED_LONG_INTEGER NewDriverAdded;
        static SIGNED_LONG_INTEGER NewPrimaryDevice;
        static SIGNED_LONG_INTEGER OnSonosDeviceUpdated;
        static SIGNED_LONG_INTEGER OnSonosDeviceOfflineStateUpdated;
        static SIGNED_LONG_INTEGER OnSonosDeviceRawOfflineStateUpdated;
        static SIGNED_LONG_INTEGER SonosDeviceDeleted;
        static SIGNED_LONG_INTEGER SonosGroupModified;
        static SIGNED_LONG_INTEGER SonosGroupAdded;
        static SIGNED_LONG_INTEGER SonosGroupRemoved;
        static SIGNED_LONG_INTEGER SystemGroupConfigurationChanged;
        static SIGNED_LONG_INTEGER SonosGroupConfigurationEventResult;
        static SIGNED_LONG_INTEGER SonosHouseholdIdChanged;
        static SIGNED_LONG_INTEGER SonosNumberOfDiscoveredHouseholdsChanged;
        static SIGNED_LONG_INTEGER SonosDiscoveryResult;
        static SIGNED_LONG_INTEGER OnDiagnosticsSonosSystemInformationChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsSystemEventInformationChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticIsolatedSonosDiscoveryResultChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsDiscoveryChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsDiscoveryModeChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsDiscoveryStateChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsDiscoveryResultChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsPrimaryDeviceChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsPrimaryDeviceNameChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsPrimaryDeviceStateChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsPrimaryDeviceConnectionFailureStateChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsDeviceStateChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsSystemStateChanged;
        static SIGNED_LONG_INTEGER OnDiagnosticsSystemConnectionFailureReasonChangedEvent;
        static SIGNED_LONG_INTEGER OnDiagnosticsSystemEventStateChangedEvent;
        static SIGNED_LONG_INTEGER OnDiagnosticsDeviceEventStateChangedEvent;
    };

namespace SonosProLibrary.Diagnostics.Events;
        // class declarations
         class OnSonosSystemDiagnosticInformationChangedEventArgs;
         class OnIsolatedSonosDiscoveryResultChangedEventArgs;
         class OnSonosDiscoveryChangedEventArgs;
         class OnSonosDiscoveryModeChangedEventArgs;
         class OnSonosDiscoveryStateChangedEventArgs;
         class OnSonosDiscoveryResultChangedEventArgs;
         class OnSonosPrimaryDeviceChangedEventArgs;
         class OnSonosPrimaryDeviceNameChangedEventArgs;
         class OnSonosPrimaryDeviceStateChangedEventArgs;
         class OnSonosPrimaryDeviceConnectionFailureStateChangedEventArgs;
         class OnSonosDeviceStateChangedEventArgs;
         class OnSonosSystemChangedEventArgs;
         class OnSonosSystemStateChangedEventArgs;
         class OnSonosSystemConnectionFailureReasonChangedEventArgs;
     class OnSonosSystemDiagnosticInformationChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnIsolatedSonosDiscoveryResultChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
        SIGNED_LONG_INTEGER Mode;
    };

     class OnSonosDiscoveryChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Mode;
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER Result;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDiscoveryModeChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Mode;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDiscoveryStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDiscoveryResultChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Result;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosPrimaryDeviceChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Name[];
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER ConnectionFailureReason;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosPrimaryDeviceNameChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Name[];
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosPrimaryDeviceStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosPrimaryDeviceConnectionFailureStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER ConnectionFailureReason;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosDeviceStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        STRING Name[];
        SIGNED_LONG_INTEGER State;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosSystemChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SystemState;
        SIGNED_LONG_INTEGER SystemConnectionFailureReason;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosSystemStateChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER SystemState;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

     class OnSonosSystemConnectionFailureReasonChangedEventArgs 
    {
        // class delegates

        // class events

        // class functions
        STRING_FUNCTION GetDescription ();
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        SIGNED_LONG_INTEGER Reason;
        SIGNED_LONG_INTEGER SonosEventType;

        // class properties
    };

namespace SonosProLibrary.Diagnostics.Enums;
        // class declarations
         class SonosDeviceState;
         class SonosDiscoveryMode;
         class SonosDiscoveryState;
         class SonosDiscoveryResult;
         class SonosSystemState;
         class SonosSystemConnectionFailureReason;
         class SonosConnectionFailureReason;
         class SonosDriverFailureResaon;
    static class SonosDeviceState // enum
    {
        static SIGNED_LONG_INTEGER NotConnected;
        static SIGNED_LONG_INTEGER Connected;
        static SIGNED_LONG_INTEGER Connecting;
        static SIGNED_LONG_INTEGER ConnectedWithEventSubscriptions;
    };

    static class SonosDiscoveryMode // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER Multicast;
        static SIGNED_LONG_INTEGER Broadcast;
        static SIGNED_LONG_INTEGER MulticastAndBroadcast;
    };

    static class SonosDiscoveryState // enum
    {
        static SIGNED_LONG_INTEGER Idle;
        static SIGNED_LONG_INTEGER MulticastInProgress;
        static SIGNED_LONG_INTEGER BroadcastInProgress;
        static SIGNED_LONG_INTEGER AutoDiscoveryEnabled;
        static SIGNED_LONG_INTEGER AutoDiscoveryInProgress;
    };

    static class SonosDiscoveryResult // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER MulticastSuccess;
        static SIGNED_LONG_INTEGER MulticastFailure;
        static SIGNED_LONG_INTEGER BroadcastSuccess;
        static SIGNED_LONG_INTEGER BroadcastFailure;
        static SIGNED_LONG_INTEGER NoDevicesFound;
        static SIGNED_LONG_INTEGER MultipleHouseHoldsFound;
        static SIGNED_LONG_INTEGER SonosBoostFound;
        static SIGNED_LONG_INTEGER SonosBridgeFound;
    };

    static class SonosSystemState // enum
    {
        static SIGNED_LONG_INTEGER SystemNotConnected;
        static SIGNED_LONG_INTEGER DiscoveringDevices;
        static SIGNED_LONG_INTEGER ConnectingToPrimaryDevice;
        static SIGNED_LONG_INTEGER ConnectedToPrimaryDevice;
        static SIGNED_LONG_INTEGER SubscribingForGroupConfigurationEvent;
        static SIGNED_LONG_INTEGER SubscribedForGroupConfigurationEvent;
        static SIGNED_LONG_INTEGER SubscribingForFavoritesEvent;
        static SIGNED_LONG_INTEGER SubscribedForFavoritesEvent;
        static SIGNED_LONG_INTEGER ConnectedAndSubscribedForSystemEvents;
        static SIGNED_LONG_INTEGER ReceivedGroupConfigurationEvent;
        static SIGNED_LONG_INTEGER AutoDiscoveryMode;
        static SIGNED_LONG_INTEGER SystemReady;
    };

    static class SonosSystemConnectionFailureReason // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER NoDevicesFound;
        static SIGNED_LONG_INTEGER HouseHoldNotFound;
        static SIGNED_LONG_INTEGER NoOnlineDevicesFound;
        static SIGNED_LONG_INTEGER PrimaryDeviceConnectionFailed;
        static SIGNED_LONG_INTEGER GroupConfigurationEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER GroupConfigurationEventSubscriptionTimeOut;
        static SIGNED_LONG_INTEGER FavoritesEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER FavoritesEventSubscriptionTimeOut;
        static SIGNED_LONG_INTEGER NoHouseholdDefined;
    };

    static class SonosConnectionFailureReason // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER ConnectionClosed;
        static SIGNED_LONG_INTEGER ConnectionTimeout;
        static SIGNED_LONG_INTEGER ConnectError;
        static SIGNED_LONG_INTEGER NumberOfDeviceConnectionsExceeded;
        static SIGNED_LONG_INTEGER SendError;
        static SIGNED_LONG_INTEGER SendTimeout;
        static SIGNED_LONG_INTEGER WebSocketErrorDetected;
    };

    static class SonosDriverFailureResaon // enum
    {
        static SIGNED_LONG_INTEGER None;
        static SIGNED_LONG_INTEGER GroupVolumeEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER PlayerVolumeEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER MetadataEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER PlaybackEventSubscriptionFailed;
        static SIGNED_LONG_INTEGER AlbumArtEventSubscriptionFailed;
    };

namespace SonosProLibrary.Logging.Enums;
        // class declarations
         class LoggerModes;
         class SonosLogMessage;
    static class LoggerModes // enum
    {
        static SIGNED_LONG_INTEGER Off;
        static SIGNED_LONG_INTEGER Info;
        static SIGNED_LONG_INTEGER Debug;
        static SIGNED_LONG_INTEGER Trace;
        static SIGNED_LONG_INTEGER Developer;
    };

    static class SonosLogMessage 
    {
        // class delegates

        // class events

        // class functions
        SIGNED_LONG_INTEGER_FUNCTION GetHashCode ();
        STRING_FUNCTION ToString ();

        // class variables
        static STRING VolumeConfigurationChangedToFixed[];
        static STRING VolumeConfigurationChangedToVariable[];
        static STRING DeviceWasAddedAsSatelliteSpeaker[];
        static STRING DiscoveryModeChangedToMulticast[];
        static STRING DiscoveryModeChangedToBroadcast[];
        static STRING DiscoveryModeChangedToMulticastAndBroadcast[];
        static STRING DiscoveryModeChangedToNone[];
        static STRING DiscoveryStateChangedToIdle[];
        static STRING DiscoveryStateChangedToMulticastInProgress[];
        static STRING DiscoveryStateChangedToBroadcastInProgress[];
        static STRING DiscoveryStateChangedToAutoDiscoveryEnabled[];
        static STRING DiscoveryStateChangedToAutoDiscoveryInProgress[];
        static STRING DiscoveryMulticastDevicesFound[];
        static STRING DiscoveryMulticastNoDevicesFound[];
        static STRING DiscoveryBroadcastDevicesFound[];
        static STRING DiscoveryBroadcastNoDevicesFound[];
        static STRING DiscoveryDevicesFound[];
        static STRING DiscoveryNoDevicesFound[];
        static STRING DiscoveryMultipleHouseholdsFound[];
        static STRING PrimaryDeviceChanged[];
        static STRING PrimaryDeviceStateChangedToConnected[];
        static STRING PrimaryDeviceStateChangedToNotConnected[];
        static STRING PrimaryDeviceStateChangedToConnecting[];
        static STRING PrimaryDeviceStateChangedToConnectedWithSubscriptions[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToNone[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToConnectionClosed[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToConnectionTimeout[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToConnectError[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToNumberOfDeviceConnectionsExceeded[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToSendError[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToSendTimeout[];
        static STRING PrimaryDeviceConnectionFailureReasonChangedToWebSocketErrorDetected[];
        static STRING SystemStateChangedToNotConnectd[];
        static STRING SystemStateChangedToDiscoveringDevices[];
        static STRING SystemStateChangedToConnectingToSystemDevice[];
        static STRING SystemStateChangedToConnectedToSystemDevice[];
        static STRING SystemStateChangedToSubscribingForGroupConfigurationEvent[];
        static STRING SystemStateChangedToSubscribedForGroupConfigurationEvent[];
        static STRING SystemStateChangedToSubscribingForFavoirtesEvent[];
        static STRING SystemStateChangedToSubscribedForFavoritesEvent[];
        static STRING SystemStateChangedToConnectedAndSubscribedForSystemEvents[];
        static STRING SystemStateChangedToReceivedGroupConfigurationEvent[];
        static STRING SystemStateChangedToAutoDiscoveryMode[];
        static STRING SystemStateChangedToSystemReady[];
        static STRING SystemConnectionFailureReasonChangedToNone[];
        static STRING SystemConnectionFailureReasonChangedToNoDevicesFound[];
        static STRING SystemConnectionFailureReasonChangedToHouseHoldNotFound[];
        static STRING SystemConnectionFailureReasonChangedToNoOnlineDevicesFound[];
        static STRING SystemConnectionFailureReasonChangedToGroupConfigurationEventSubscriptionFailed[];
        static STRING SystemConnectionFailureReasonChangedToGroupConfigurationEventSubscriptionTimeOut[];
        static STRING SystemConnectionFailureReasonChangedToFavoritesEventSubscriptionFailed[];
        static STRING SystemConnectionFailureReasonChangedToFavoritesEventSubscriptionTimeOut[];
        static STRING PrimaryDeviceSubscibedForGroupConfigurationEvent[];
        static STRING PrimaryDeviceNotSubscibedForGroupConfigurationEvent[];
        static STRING PrimaryDeviceSubscibedForFavoritesEvent[];
        static STRING PrimaryDeviceNotSubscibedForFavoritesEvent[];
        static STRING PrimaryHouseholdChanged[];
        static STRING FailedToLoadDeletedFavorite[];

        // class properties
    };

namespace SonosProLibrary.Devices;
        // class declarations

namespace SonosProLibrary.System;
        // class declarations

namespace SonosProLibrary.Diagnostics;
        // class declarations

