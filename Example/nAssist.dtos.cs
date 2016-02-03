/* Options:
Date: 2016-02-02 16:57:47
Version: 4.046
BaseUrl: http://dev.nassist-test.com/api

//GlobalNamespace: 
//MakePartial: True
//MakeVirtual: True
//MakeDataContractsExtensible: False
//AddReturnMarker: True
//AddDescriptionAsComments: True
//AddDataContractAttributes: False
//AddIndexesToDataMembers: False
//AddGeneratedCodeAttributes: False
//AddResponseStatus: False
//AddImplicitVersion: 
//InitializeCollections: True
//IncludeTypes: 
//ExcludeTypes: 
//AddDefaultXmlNamespace: http://schemas.servicestack.net/types
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;
using nassist.ServiceModel.Camera;
using nassist.ServiceInterface.Services;
using nassist.ServiceModel;
using nassist.Azure.CloudTableStorage.Tables;
using nassist.ServiceModel.Prosyst;
using nassist.Shared;
using nassist.ServiceModel.UPnP;
using nassist.Shared.SensorData;
using nassist.Shared.Constants;
using Microsoft.WindowsAzure.Storage.Table;


namespace Microsoft.WindowsAzure.Storage.Table
{

    public partial class TableEntity
    {
        public virtual string PartitionKey { get; set; }
        public virtual string RowKey { get; set; }
        public virtual DateTimeOffset Timestamp { get; set; }
        public virtual string ETag { get; set; }
    }
}

namespace nassist.Azure.CloudTableStorage.Tables
{

    public partial class AzureCameraConsumption
        : TableEntity
    {
        public virtual long Bytes { get; set; }
    }

    public partial class AzureEvent
        : TableEntity
    {
        public virtual string Type { get; set; }
        public virtual string Subtype { get; set; }
        public virtual bool Pending { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual DateTime? LastUpdate { get; set; }
        public virtual string InstallationId { get; set; }
        public virtual string Installation { get; set; }
        public virtual string Description { get; set; }
        public virtual string DescriptionArgs { get; set; }
        public virtual string Comments { get; set; }
        public virtual string TriggerId { get; set; }
        public virtual string TriggerType { get; set; }
        public virtual string TriggerName { get; set; }
        public virtual string LastImageName { get; set; }
        public virtual string CameraTrigger { get; set; }
        public virtual string TranslatedDescription { get; set; }
    }
}

namespace nassist.ServiceInterface.Services
{

    public partial class AssignableInstallationsResponse
        : ResponseBase
    {
        public AssignableInstallationsResponse()
        {
            Installations = new List<AssignableInstallation>{};
        }

        public virtual string Id { get; set; }
        public virtual List<AssignableInstallation> Installations { get; set; }
    }

    [Route("/users/{Id}/installations/assignable", "GET")]
    public partial class AssignableUserInstallations
        : IReturn<AssignableInstallationsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    [Route("/cameras/{Id}/consumption", "POST")]
    public partial class CameraConsumption
        : IReturn<CameraConsumptionResponse>
    {
        [ApiMember(Name="Id", Description="CameraId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Bytes", Description="Bytes consumed", ParameterType="body", DataType="long", IsRequired=true, Verb="POST")]
        public virtual long Bytes { get; set; }
    }

    public partial class CameraConsumptionResponse
        : ResponseBase
    {
    }

    [Route("/cameras/{Id}/details", "GET")]
    [Route("/cameras/{Id}", "DELETE")]
    public partial class CameraDetails
        : IReturn<CameraDetailsResponse>
    {
        [ApiMember(Name="Id", Description="Camera id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="InstallationId", ParameterType="body", DataType="string", IsRequired=true, Verb="DELETE")]
        public virtual string InstallationId { get; set; }
    }

    public partial class CameraDetailsResponse
        : ResponseBase
    {
        public virtual Camera Camera { get; set; }
    }

    [Route("/cameras/{Id}/history", "GET")]
    public partial class CameraHistory
        : IReturn<CameraPhotoResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromPhoto", Description="Initial Photo", ParameterType="query", DataType="int", IsRequired=true)]
        public virtual int FromPhoto { get; set; }

        [ApiMember(Name="PageSize", Description="Page Size", ParameterType="query", DataType="int", IsRequired=true)]
        public virtual int PageSize { get; set; }
    }

    [Route("/cameras/{Id}/installations", "GET")]
    public partial class CameraInstallations
        : IReturn<AssignableInstallationsResponse>
    {
        [ApiMember(Name="Id", Description="CameraId", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    [Route("/cameras/{Id}/lastphoto", "GET")]
    [Route("/cameras/{Id}/lastphoto", "POST")]
    public partial class CameraLastPhoto
        : IReturn<CameraLastPhotoResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="Installation Id", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string InstallationId { get; set; }
    }

    public partial class CameraLastPhotoResponse
        : ResponseBase
    {
        public virtual string ResponseString { get; set; }
        public virtual string LastPhotoName { get; set; }
        public virtual DateTime? LastPhotoDate { get; set; }
    }

    [Route("/cameranodes/details", "GET")]
    [Route("/cameranodes", "POST")]
    [Route("/cameranodes", "DELETE")]
    public partial class CameraNodes
        : IReturn<CameraNodesResponse>
    {
        [ApiMember(Name="CameraNode", Description="Camera Node", ParameterType="body", DataType="CameraNode", IsRequired=true, Verb="POST")]
        [ApiMember(Name="CameraNode", Description="Camera Node", ParameterType="body", DataType="CameraNode", IsRequired=true, Verb="DELETE")]
        public virtual CameraNode CameraNode { get; set; }
    }

    public partial class CameraNodesResponse
        : ResponseBase
    {
        public CameraNodesResponse()
        {
            CameraNodes = new List<CameraNode>{};
        }

        public virtual List<CameraNode> CameraNodes { get; set; }
    }

    [Route("/cameras/oldphotos", "DELETE")]
    public partial class CameraOldPhoto
        : IReturn<CameraOldPhotoResponse>
    {
    }

    public partial class CameraOldPhotoResponse
        : ResponseBase
    {
    }

    [Route("/cameras/{Id}/photos", "GET")]
    [Route("/cameras/{Id}/photos", "POST")]
    [Route("/cameras/{Id}/photos", "DELETE")]
    public partial class CameraPhoto
        : IReturn<CameraPhotoResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual DateTime Date { get; set; }
        public virtual string Base64 { get; set; }
        public virtual string Trigger { get; set; }
        public virtual DateTime DeleteBeforeDate { get; set; }
        public virtual DateTime? FromDate { get; set; }
        public virtual DateTime? ToDate { get; set; }
    }

    [Route("/cameras/{Id}/photo", "GET")]
    [Route("/cameras/{Id}/photo", "DELETE")]
    public partial class CameraPhotoBlob
        : IReturn<CameraPhotoBlobResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string TriggerId { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public partial class CameraPhotoBlobResponse
        : ResponseBase
    {
        public virtual string PhotoNameWithSAS { get; set; }
    }

    public partial class CameraPhotoResponse
        : ResponseBase
    {
        public CameraPhotoResponse()
        {
            Photos = new List<Photo>{};
        }

        public virtual List<Photo> Photos { get; set; }
        public virtual string CameraId { get; set; }
        public virtual string CameraName { get; set; }
        public virtual int NumPages { get; set; }
    }

    [Route("/cameras", "GET")]
    [Route("/cameras", "POST")]
    [Route("/cameras", "PUT")]
    public partial class Cameras
        : IReturn<CamerasResponse>
    {
        public virtual Camera Camera { get; set; }
        public virtual bool UpdateOnGateway { get; set; }
        public virtual string InstallationId { get; set; }
    }

    [Route("/cameras", "PATCH")]
    public partial class CamerasPatch
        : QueryBase<Camera>, IReturn<QueryResponse<Camera>>
    {
        public CamerasPatch()
        {
            Fields = new string[]{};
        }

        [ApiMember(Verb="PATCH", ParameterType="body", Name="Camera", Description="Camera object", DataType="Camera", IsRequired=true)]
        public virtual Camera Camera { get; set; }

        [ApiMember(Name="fields", Description="Fields to update", ParameterType="query", DataType="string[]", IsRequired=true, Verb="PATCH")]
        public virtual string[] Fields { get; set; }
    }

    public partial class CamerasResponse
        : ResponseBase
    {
        public CamerasResponse()
        {
            Cameras = new List<Camera>{};
        }

        public virtual List<Camera> Cameras { get; set; }
        public virtual Camera NewCamera { get; set; }
    }

    [Route("/cameras/{Id}/users", "GET")]
    public partial class CameraUsers
        : IReturn<CameraUsersResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class CameraUsersResponse
        : ResponseBase
    {
        public CameraUsersResponse()
        {
            CameraUsers = new List<UserAuth>{};
        }

        public virtual List<UserAuth> CameraUsers { get; set; }
    }

    [Route("/cameras/{Id}/videoconsumption", "GET")]
    public partial class CameraVideoConsumption
        : IReturn<CameraVideoConsumptionResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class CameraVideoConsumptionResponse
        : ResponseBase
    {
        public CameraVideoConsumptionResponse()
        {
            VideoConsumption = new List<AzureCameraConsumption>{};
        }

        public virtual List<AzureCameraConsumption> VideoConsumption { get; set; }
    }

    [Route("/cameras/{Id}/video/endpoint", "GET")]
    public partial class CameraVideoStreamingEndpoint
        : IReturn<CameraVideoStreamingEndpointResponse>
    {
        [ApiMember(Name="Id", Description="Camera Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Installation Id", Description="Installation Id", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string InstallationId { get; set; }
    }

    public partial class CameraVideoStreamingEndpointResponse
        : ResponseBase
    {
        public virtual string Endpoint { get; set; }
    }

    [Route("/events/{UserId}/{EventId}/lastPhoto/", "GET")]
    public partial class EventPhoto
    {
        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserId { get; set; }

        [ApiMember(Name="EventId", Description="Event id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string EventId { get; set; }
    }

    [Route("/events", "POST")]
    [Route("/events/{Type}", "DELETE")]
    public partial class Events
        : IReturn<EventsResponse>
    {
        public Events()
        {
            UserIds = new List<int>{};
        }

        public virtual AzureEvent Event { get; set; }
        public virtual List<int> UserIds { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime? DateFrom { get; set; }
        public virtual DateTime? DateTo { get; set; }
    }

    [Route("/users/{UserId}/events/", "GET")]
    [Route("/users/{UserId}/events/{Type}", "GET")]
    [Route("/users/{UserId}/events/{Type}", "DELETE")]
    public partial class EventsBatch
        : IReturn<EventsBatchResponse>
    {
        [ApiMember(Name="UserId", Description="Show events for specified user id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="UserId", Description="Show events for specified user id", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string UserId { get; set; }

        [ApiMember(Name="Type", Description="Only return events of specified type", ParameterType="path", DataType="string", Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Type", Description="Deletes events of specified type", ParameterType="path", DataType="string", Verb="DELETE", ExcludeInSchema=true)]
        public virtual string Type { get; set; }

        [ApiMember(Name="Pending", Description="Only return pending events", ParameterType="query", DataType="bool?", Verb="GET")]
        public virtual bool? Pending { get; set; }

        [ApiMember(Name="Page", Description="Page number", ParameterType="query", DataType="int?", Verb="GET")]
        public virtual int? Page { get; set; }

        [ApiMember(Name="PageSize", Description="Page size", ParameterType="query", DataType="int?", Verb="GET")]
        public virtual int? PageSize { get; set; }

        public virtual DateTime? DateFrom { get; set; }
        public virtual DateTime? DateTo { get; set; }
    }

    public partial class EventsBatchResponse
        : ResponseBase
    {
        public EventsBatchResponse()
        {
            Events = new List<AzureEvent>{};
        }

        public virtual List<AzureEvent> Events { get; set; }
    }

    [Route("/users/{UserId}/events/count", "GET")]
    [Route("/users/{UserId}/events/{Type}/count", "GET")]
    public partial class EventsCount
        : IReturn<EventsCountResponse>
    {
        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserId { get; set; }

        [ApiMember(Name="Type", Description="Events type filter", ParameterType="path", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }

        [ApiMember(Name="Pending", Description="Pending events filter", ParameterType="query", DataType="bool", Verb="GET")]
        public virtual bool Pending { get; set; }

        [ApiMember(Name="DateFrom", Description="Date from filter", ParameterType="query", DataType="DateTime?", Verb="GET")]
        public virtual DateTime? DateFrom { get; set; }

        [ApiMember(Name="DateTo", Description="Date to filter", ParameterType="query", DataType="DateTime?", Verb="GET")]
        public virtual DateTime? DateTo { get; set; }
    }

    public partial class EventsCountResponse
        : ResponseBase
    {
        public EventsCountResponse()
        {
            EventGroupsCount = new Dictionary<string, int>{};
        }

        public virtual Dictionary<string, int> EventGroupsCount { get; set; }
        public virtual int TotalCount { get; set; }
    }

    [Route("/users/{UserId}/events/{EventId}/delete", "DELETE")]
    public partial class EventsDelete
    {
        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string UserId { get; set; }

        [ApiMember(Name="EventId", Description="Event id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual Guid EventId { get; set; }
    }

    [Route("/users/{UserId}/events/{EventType}/pending", "PUT")]
    public partial class EventsForTypePendingBatch
        : IReturn<EventsPendingResponse>
    {
        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="PUT", ExcludeInSchema=true)]
        public virtual string UserId { get; set; }

        [ApiMember(Name="Type", Description="Event type", ParameterType="path", DataType="string", IsRequired=true, Verb="PUT", ExcludeInSchema=true)]
        public virtual string EventType { get; set; }
    }

    [Route("/events/{UserId}/all", "GET")]
    public partial class EventsGeneralBatch
        : IReturn<EventsGeneralBatchResponse>
    {
        [ApiMember(Name="UserId", Description="Show events for specified user id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserId { get; set; }

        [ApiMember(Name="Pending", Description="Only return pending events", ParameterType="query", DataType="bool?", Verb="GET")]
        public virtual bool? Pending { get; set; }

        [ApiMember(Name="Page", Description="Page number", ParameterType="query", DataType="int?", Verb="GET")]
        public virtual int? Page { get; set; }

        [ApiMember(Name="PageSize", Description="Page size", ParameterType="query", DataType="int?", Verb="GET")]
        public virtual int? PageSize { get; set; }

        [ApiMember(Name="DateFrom", Description="DateFrom", ParameterType="query", DataType="DateTime?", Verb="GET")]
        public virtual DateTime? DateFrom { get; set; }

        [ApiMember(Name="DateTo", Description="DateTo", ParameterType="query", DataType="DateTime?", Verb="GET")]
        public virtual DateTime? DateTo { get; set; }
    }

    public partial class EventsGeneralBatchResponse
        : ResponseBase
    {
        public EventsGeneralBatchResponse()
        {
            EventGroups = new Dictionary<string, List<AzureEvent>>{};
            EventGroupsCount = new Dictionary<string, int>{};
        }

        public virtual Dictionary<string, List<AzureEvent>> EventGroups { get; set; }
        public virtual Dictionary<string, int> EventGroupsCount { get; set; }
        public virtual int TotalCount { get; set; }
    }

    [Route("/events/old", "PATCH")]
    [Route("/events/old", "DELETE")]
    public partial class EventsOld
        : IReturn<EventsOldResponse>
    {
    }

    public partial class EventsOldResponse
        : ResponseBase
    {
    }

    [Route("/users/{UserId}/events/{EventId}/togglePending", "PATCH")]
    public partial class EventsPending
        : IReturn<EventsPendingResponse>
    {
        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual string UserId { get; set; }

        [ApiMember(Name="EventId", Description="Events to be updated", ParameterType="path", DataType="string", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual string EventId { get; set; }
    }

    [Route("/users/{UserId}/events/pending/", "PATCH")]
    public partial class EventsPendingBatch
        : IReturn<EventsPendingResponse>
    {
        public EventsPendingBatch()
        {
            EventIds = new List<string>{};
        }

        [ApiMember(Name="UserId", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual string UserId { get; set; }

        [ApiMember(Name="EventIds", Description="Events to be updated", ParameterType="body", DataType="List<string>", IsRequired=true, Verb="PATCH")]
        public virtual List<string> EventIds { get; set; }
    }

    public partial class EventsPendingResponse
        : ResponseBase
    {
    }

    public partial class EventsResponse
        : ResponseBase
    {
        public virtual AzureEvent Event { get; set; }
    }

    [Route("/gateways/{Id}/activate", "GET")]
    [Route("/gateways/{Id}/activate", "POST")]
    public partial class GatewayActivate
        : IReturn<GatewayActivateResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayActivateResponse
        : ResponseBase
    {
        public virtual bool Activated { get; set; }
    }

    [Route("/gateways/{Id}/actuator/{ActuatorId}/toggle", "POST")]
    public partial class GatewayActuatorToggle
        : IReturn<GatewayActuatorToggleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ActuatorId", Description="ActuatorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string ActuatorId { get; set; }

        public virtual string Value { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayActuatorToggleResponse
        : ResponseBase
    {
        public virtual bool ToggleSuccess { get; set; }
        public virtual bool CommunicationSuccess { get; set; }
    }

    [Route("/gateways/{Id}/addsupportedcamera", "POST")]
    public partial class GatewayAddCamera
        : IReturn<GatewayAddCameraResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Camera", Description="Camera", ParameterType="body", DataType="Camera", IsRequired=true, Verb="POST")]
        public virtual Camera Camera { get; set; }
    }

    public partial class GatewayAddCameraResponse
        : ResponseBase
    {
        public virtual bool AddSuccess { get; set; }
    }

    [Route("/gateways/{Id}/addschedule", "POST")]
    public partial class GatewayAddSchedule
        : IReturn<GatewayAddScheduleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Type { get; set; }
        public virtual SchedulerPoint Point { get; set; }
    }

    public partial class GatewayAddScheduleResponse
        : ResponseBase
    {
        public virtual bool AddSuccess { get; set; }
    }

    [Route("/gateways/{Id}/addwmbussensor", "POST")]
    public partial class GatewayAddWMBusSensor
        : IReturn<GatewayAddWMBusSensorResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string SerialId { get; set; }
        public virtual string Key { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string Model { get; set; }
        public virtual string Version { get; set; }
        public virtual string SensorId { get; set; }
        public virtual double AccumulatedScale { get; set; }
        public virtual double InstantScale { get; set; }
    }

    public partial class GatewayAddWMBusSensorResponse
        : ResponseBase
    {
        public virtual bool AddWMBusSensorSuccess { get; set; }
    }

    [Route("/gateways/{Id}/alive", "GET")]
    [Route("/gateways/{Id}/alive", "POST")]
    public partial class GatewayAlive
        : IReturn<GatewayAliveResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="Alive", Description="Gateway Alive", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string Alive { get; set; }
    }

    public partial class GatewayAliveResponse
        : ResponseBase
    {
        public GatewayAliveResponse()
        {
            AliveTimes = new List<DateTime>{};
        }

        public virtual List<DateTime> AliveTimes { get; set; }
    }

    [Route("/gateways/{Id}/apiurl", "POST")]
    public partial class GatewayApiURL
        : IReturn<GatewayApiURLResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ApiURL", Description="Api URL", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string ApiURL { get; set; }
    }

    public partial class GatewayApiURLResponse
        : ResponseBase
    {
        public virtual bool SetApiURLSuccess { get; set; }
    }

    [Route("/gateways/{Id}/bypass/{SensorId}", "POST")]
    public partial class GatewayBypassSensor
        : IReturn<GatewayBypassSensorResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="BypassStatus", Description="Bypass Status (true or false)", ParameterType="body", DataType="bool", IsRequired=true)]
        public virtual bool BypassStatus { get; set; }
    }

    public partial class GatewayBypassSensorResponse
        : ResponseBase
    {
        public virtual CommandResponseWrapper<bool> SensorBypassSuccess { get; set; }
    }

    [Route("/gateways/{Id}/camera/{CameraId}/picture", "POST")]
    public partial class GatewayCameraPicture
        : IReturn<GatewayCameraPictureResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="CameraId", Description="CameraId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string CameraId { get; set; }

        [ApiMember(Name="UserId", Description="UserId", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string UserId { get; set; }
    }

    public partial class GatewayCameraPictureResponse
        : ResponseBase
    {
        public virtual bool PictureSuccess { get; set; }
    }

    [Route("/gateways/{Id}/camera/poll", "GET")]
    public partial class GatewayCameraPollAll
        : IReturn<GatewayCameraPollAllResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayCameraPollAllResponse
        : ResponseBase
    {
        public GatewayCameraPollAllResponse()
        {
            CameraPollAllStatus = new Dictionary<string, bool>{};
        }

        public virtual Dictionary<string, bool> CameraPollAllStatus { get; set; }
    }

    [Route("/gateways/{Id}/camera/{CameraId}", "DELETE")]
    public partial class GatewayCameraRemove
        : IReturn<GatewayCameraRemoveResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="CameraId", Description="CameraId", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string CameraId { get; set; }
    }

    public partial class GatewayCameraRemoveResponse
        : ResponseBase
    {
        public virtual bool RemoveSuccess { get; set; }
    }

    [Route("/gateways/{Id}/camera/{CameraId}/video", "POST")]
    public partial class GatewayCameraVideo
        : IReturn<GatewayCameraVideoResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="CameraId", Description="CameraId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string CameraId { get; set; }
    }

    public partial class GatewayCameraVideoResponse
        : ResponseBase
    {
        public virtual string Endpoint { get; set; }
    }

    [Route("/gateways/{Id}/checkupdate", "GET")]
    public partial class GatewayCheckUpdate
        : IReturn<GatewayCheckUpdateResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayCheckUpdateResponse
        : ResponseBase
    {
        public virtual bool newUpdate { get; set; }
    }

    [Route("/gateways/{Id}/csstring", "POST")]
    public partial class GatewayCloudStorageString
        : IReturn<GatewayCloudStorageStringResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="CSString", Description="CS Connection String", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string CSString { get; set; }
    }

    public partial class GatewayCloudStorageStringResponse
        : ResponseBase
    {
        public virtual bool SetCloudStorageStringSuccess { get; set; }
    }

    [Route("/gateways/{Id}/sensor/{sensorId}/configure", "GET")]
    public partial class GatewayConfigureSensor
        : IReturn<GatewayConfigureSensorResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="SensorId", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="SensorConfiguration", Description="Sensor Configuration", ParameterType="body", DataType="SensorConfiguration", IsRequired=true)]
        public virtual SensorConfiguration Configuration { get; set; }
    }

    public partial class GatewayConfigureSensorResponse
        : ResponseBase
    {
        public virtual bool ConfigurationSuccess { get; set; }
    }

    [Route("/gateways/{Id}/backup/create", "POST")]
    public partial class GatewayCreateBackup
        : IReturn<GatewayCreateBackupResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayCreateBackupResponse
        : ResponseBase
    {
        public virtual bool CreateSuccess { get; set; }
    }

    [Route("/gateways/{Id}/backup/createsystem", "POST")]
    public partial class GatewayCreateSystemBackup
        : IReturn<GatewayCreateSystemBackupResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayCreateSystemBackupResponse
        : ResponseBase
    {
        public virtual bool CreateSuccess { get; set; }
    }

    [Route("/gateways/date/sync", "GET")]
    public partial class GatewayDateSync
        : IReturn<GatewayDateSyncResponse>
    {
    }

    public partial class GatewayDateSyncResponse
        : ResponseBase
    {
        public virtual long DateUTCEpoch { get; set; }
    }

    [Route("/gateways/{Id}/devices", "GET")]
    public partial class GatewayDevices
        : IReturn<GatewayDevicesResponse>
    {
        [ApiMember(Name="Id", Description="Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayDevicesResponse
        : ResponseBase
    {
        public GatewayDevicesResponse()
        {
            Devices = new List<Device>{};
        }

        public virtual List<Device> Devices { get; set; }
    }

    [Route("/gateways/{Id}/dimmer/{DimmerId}/toggle", "POST")]
    public partial class GatewayDimmerToggle
        : IReturn<GatewayDimmerToggleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="DimmerId", Description="DimmerId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string DimmerId { get; set; }

        public virtual int Value { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayDimmerToggleResponse
        : ResponseBase
    {
        public virtual bool DimmerSuccess { get; set; }
        public virtual bool CommunicationSuccess { get; set; }
    }

    [Route("/gateways/{Id}/doorLock/{SensorId}/setpassword", "POST")]
    public partial class GatewayDoorLockSetPassword
        : IReturn<GatewayDoorLockSetPasswordResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        public virtual string Password { get; set; }
    }

    public partial class GatewayDoorLockSetPasswordResponse
        : ResponseBase
    {
        public virtual bool SetPasswordSuccess { get; set; }
    }

    [Route("/gateways/{Id}/doorLock/{SensorId}/toggle", "POST")]
    public partial class GatewayDoorLockToggle
        : IReturn<GatewayDoorLockToggleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        public virtual bool Lock { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayDoorLockToggleResponse
        : ResponseBase
    {
        public virtual bool ToggleSuccess { get; set; }
        public virtual bool CommunicationSuccess { get; set; }
    }

    [Route("/gateways/{Id}/gwversion", "POST")]
    public partial class GatewayGWVersion
        : IReturn<GWVersionResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="GWVersion", Description="GW Version", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string GWVersion { get; set; }
    }

    [Route("/gateways/{Id}/ipscan", "GET")]
    public partial class GatewayIPScan
        : IReturn<GatewayIPScanResponse>
    {
        [ApiMember(Name="Id", Description="Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayIPScanResponse
        : ResponseBase
    {
        public GatewayIPScanResponse()
        {
            ScanInfo = new List<IPScanInfo>{};
        }

        public virtual List<IPScanInfo> ScanInfo { get; set; }
    }

    [Route("/gateways/{Id}/livemode", "POST")]
    public partial class GatewayLiveMode
        : IReturn<GatewayLiveModeResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayLiveModeResponse
        : ResponseBase
    {
        public virtual bool LiveModeSuccess { get; set; }
    }

    [Route("/gateways/{Id}/logging/state", "GET")]
    public partial class GatewayLoggingState
        : IReturn<GatewayLoggingStateResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayLoggingStateResponse
        : ResponseBase
    {
        public virtual bool? IsLoggingActive { get; set; }
    }

    [Route("/gateways/{Id}/logging/toggle", "POST")]
    public partial class GatewayLoggingToggle
        : IReturn<GatewayLoggingToggleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="LoggingState", Description="LoggingState", ParameterType="body", DataType="bool", IsRequired=true)]
        public virtual bool LoggingState { get; set; }
    }

    public partial class GatewayLoggingToggleResponse
        : ResponseBase
    {
        public virtual bool LoggingToggleSuccess { get; set; }
    }

    [Route("/gateways/{Id}/logs/update", "POST")]
    [Route("/gateways/{Id}/logs", "PUT")]
    [Route("/gateways/{Id}/logs", "GET")]
    public partial class GatewayLogs
        : IReturn<GatewayLogsResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="LogName", Description="LogName", ParameterType="body", DataType="string", IsRequired=true, Verb="PUT")]
        public virtual string LogName { get; set; }
    }

    public partial class GatewayLogsResponse
        : ResponseBase
    {
        public virtual bool LogsRequestSuccess { get; set; }
        public virtual string LogDownloadLink { get; set; }
    }

    [Route("/gateways/{Id}/network/close", "POST")]
    public partial class GatewayNetworkClose
        : IReturn<GatewayNetworkCloseResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayNetworkCloseResponse
        : ResponseBase
    {
        public virtual bool CloseSuccess { get; set; }
    }

    [Route("/gateways/{Id}/networkmaintenance", "POST")]
    public partial class GatewayNetworkMaintenance
        : IReturn<GatewayNetworkMaintenanceResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayNetworkMaintenanceResponse
        : ResponseBase
    {
        public virtual bool NetworkMaintenanceSuccess { get; set; }
    }

    [Route("/gateways/{Id}/network/status", "GET")]
    public partial class GatewayNetworkStatus
        : IReturn<GatewayNetworkStatusResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayNetworkStatusResponse
        : ResponseBase
    {
        public virtual string NetworkStatus { get; set; }
    }

    [Route("/gateways/{Id}/notify/addorremovesensor", "POST")]
    public partial class GatewayNotifyAddOrRemoveSensor
        : IReturn<GatewayNotifyAddOrRemoveSensorResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="IsAdded", Description="Sensor Addition or Removal indicator", ParameterType="body", DataType="bool", IsRequired=true)]
        public virtual bool IsAdded { get; set; }
    }

    public partial class GatewayNotifyAddOrRemoveSensorResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/notify/backupcreated", "POST")]
    public partial class GatewayNotifyBackupCreated
        : IReturn<GatewayNotifyBackupCreatedResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="IsCreated", Description="Backup creation indicator", ParameterType="body", DataType="bool", IsRequired=true)]
        public virtual bool IsCreated { get; set; }
    }

    public partial class GatewayNotifyBackupCreatedResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/notify/mainthermostatchanged", "POST")]
    public partial class GatewayNotifyMainThermostatChanged
        : IReturn<GatewayNotifyMainThermostatChangedResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor Identifier", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string SensorId { get; set; }
    }

    public partial class GatewayNotifyMainThermostatChangedResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/notify/networkstatus", "POST")]
    public partial class GatewayNotifyNetworkStatus
        : IReturn<GatewayNotifyNetworkStatusResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Status", Description="Network Status", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string Status { get; set; }
    }

    public partial class GatewayNotifyNetworkStatusResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/notify/sensorconfigured", "POST")]
    public partial class GatewayNotifySensorConfigured
        : IReturn<GatewayNotifySensorConfiguredResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor Identifier", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string SensorId { get; set; }
    }

    public partial class GatewayNotifySensorConfiguredResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/notify/systembackupcreated", "POST")]
    public partial class GatewayNotifySystemBackupCreated
        : IReturn<GatewayNotifySystemBackupCreatedResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="IsCreated", Description="Backup creation indicator", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual bool IsCreated { get; set; }
    }

    public partial class GatewayNotifySystemBackupCreatedResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/pauseschedule", "POST")]
    public partial class GatewayPauseSchedule
        : IReturn<GatewayPauseScheduleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Type { get; set; }
        public virtual bool Pause { get; set; }
    }

    public partial class GatewayPauseScheduleResponse
        : ResponseBase
    {
        public virtual bool PauseSuccess { get; set; }
    }

    [Route("/gateways/{Id}/ping", "POST")]
    public partial class GatewayPing
        : IReturn<GatewayPingResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayPingResponse
        : ResponseBase
    {
        public virtual bool Connection { get; set; }
    }

    [Route("/gateways/{Id}/Sensors/{SensorId}/readregister", "GET")]
    public partial class GatewayReadRegister
        : IReturn<GatewayReadRegisterResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="RegNum", Description="Register number to read", ParameterType="query", DataType="int", IsRequired=true)]
        public virtual int RegNum { get; set; }
    }

    public partial class GatewayReadRegisterResponse
        : ResponseBase
    {
        public virtual int? RegisterValue { get; set; }
    }

    [Route("/gateways/{Id}/Sensors/{SensorId}/readregisters", "GET")]
    public partial class GatewayReadRegisters
        : IReturn<GatewayReadRegistersResponse>
    {
        public GatewayReadRegisters()
        {
            RegNums = new List<int>{};
        }

        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="RegNums", Description="Register numbers to read", ParameterType="query", DataType="List<int>", IsRequired=true)]
        public virtual List<int> RegNums { get; set; }
    }

    public partial class GatewayReadRegistersResponse
        : ResponseBase
    {
        public GatewayReadRegistersResponse()
        {
            RegisterValues = new Dictionary<string, int>{};
        }

        public virtual Dictionary<string, int> RegisterValues { get; set; }
    }

    [Route("/gateways/{Id}/register", "POST")]
    public partial class GatewayRegister
        : IReturn<GatewayRegisterResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual string Type { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string GWVersion { get; set; }
    }

    public partial class GatewayRegisterResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/removeschedule", "POST")]
    public partial class GatewayRemoveSchedule
        : IReturn<GatewayRemoveScheduleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Type { get; set; }
        public virtual SchedulerPoint Point { get; set; }
    }

    public partial class GatewayRemoveScheduleResponse
        : ResponseBase
    {
        public virtual bool RemoveSuccess { get; set; }
    }

    [Route("/gateways/{Id}/sensor/{SensorId}/removeforce", "POST")]
    public partial class GatewayRemoveSensorForce
        : IReturn<GatewayRemoveSensorForceResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="SensorId", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }
    }

    public partial class GatewayRemoveSensorForceResponse
        : ResponseBase
    {
        public virtual bool RemoveSuccess { get; set; }
    }

    [Route("/gateways/{Id}/restart", "POST")]
    public partial class GatewayRestart
        : IReturn<GatewayRestartResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayRestartResponse
        : ResponseBase
    {
        public virtual bool RestartSuccess { get; set; }
    }

    [Route("/gateways/{Id}/backup/restore", "POST")]
    public partial class GatewayRestoreBackup
        : IReturn<GatewayRestoreBackupResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayRestoreBackupResponse
        : ResponseBase
    {
        public virtual bool RestoreSuccess { get; set; }
    }

    [Route("/gateways/{Id}/backup/restoresystem", "POST")]
    public partial class GatewayRestoreSystemBackup
        : IReturn<GatewayRestoreSystemBackupResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayRestoreSystemBackupResponse
        : ResponseBase
    {
        public virtual bool RestoreSuccess { get; set; }
    }

    [Route("/gateways/{Id}/scheduler/poll", "GET")]
    public partial class GatewaySchedulerPoll
        : IReturn<GatewaySchedulerPollResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewaySchedulerPollResponse
        : ResponseBase
    {
        public GatewaySchedulerPollResponse()
        {
            SchedulerInstances = new List<SchedulerInstance>{};
        }

        public virtual List<SchedulerInstance> SchedulerInstances { get; set; }
    }

    [Route("/gateways/{Id}/sensor/{sensorId}/poll", "GET")]
    public partial class GatewaySensorPoll
        : IReturn<GatewaySensorPollResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }
    }

    [Route("/gateways/{Id}/sensor/poll", "GET")]
    public partial class GatewaySensorPollAll
        : IReturn<GatewaySensorPollAllResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewaySensorPollAllResponse
        : ResponseBase
    {
        public GatewaySensorPollAllResponse()
        {
            SensorPollAllStatus = new Dictionary<string, SensorPollStatus>{};
        }

        public virtual Dictionary<string, SensorPollStatus> SensorPollAllStatus { get; set; }
    }

    public partial class GatewaySensorPollResponse
        : ResponseBase
    {
        public virtual SensorPollStatus SensorPollStatus { get; set; }
    }

    [Route("/gateways/{Id}/sbstring", "POST")]
    public partial class GatewayServiceBusString
        : IReturn<GatewayServiceBusStringResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SBString", Description="SB Connection String", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string SBString { get; set; }
    }

    public partial class GatewayServiceBusStringResponse
        : ResponseBase
    {
        public virtual bool SetServiceBusStringSuccess { get; set; }
    }

    [Route("/gateways/{Id}/addonesensormode", "POST")]
    public partial class GatewaySetAddOneSensorMode
        : IReturn<GatewaySetAddOneSensorModeResponse>
    {
        [ApiMember(Name="Id", Description="Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewaySetAddOneSensorModeResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/removeonesensormode", "POST")]
    public partial class GatewaySetRemoveOneSensorMode
        : IReturn<GatewaySetRemoveOneSensorModeResponse>
    {
        [ApiMember(Name="Id", Description="Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewaySetRemoveOneSensorModeResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/securityStatus", "POST")]
    public partial class GatewaySetSecurityStatus
        : IReturn<GatewaySetSecurityStatusResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string SecurityStatus { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewaySetSecurityStatusResponse
        : ResponseBase
    {
        public virtual bool SetSecurityStatusSuccess { get; set; }
    }

    [Route("/gateways/{Id}/shutdown", "POST")]
    public partial class GatewayShutdown
        : IReturn<GatewayShutdownResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayShutdownResponse
        : ResponseBase
    {
        public virtual bool ShutdownSuccess { get; set; }
    }

    [Route("/gateways/{Id}/statuses", "GET")]
    [Route("/gateways/{Id}/statuses", "POST")]
    public partial class GatewayStatuses
        : IReturn<GatewayStatusesResponse>
    {
        public GatewayStatuses()
        {
            StatusPoints = new List<StatusPoint>{};
        }

        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="StatusPoints", Description="Collection of datapoints to insert", ParameterType="body", DataType="List<StatusPoint>", IsRequired=true, Verb="POST")]
        public virtual List<StatusPoint> StatusPoints { get; set; }
    }

    public partial class GatewayStatusesResponse
        : ResponseBase
    {
        public GatewayStatusesResponse()
        {
            Statuses = new List<StatusPoint>{};
        }

        public virtual List<StatusPoint> Statuses { get; set; }
    }

    [Route("/gateways/{Id}/synchronize/sensordata", "POST")]
    public partial class GatewaySynchronizeSensorData
        : IReturn<GatewaySynchronizeSensorDataResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewaySynchronizeSensorDataResponse
        : ResponseBase
    {
        public virtual bool SynchronizeSuccess { get; set; }
    }

    [Route("/gateways/{Id}/backup/system", "GET")]
    public partial class GatewaySystemBackup
        : IReturn<GatewaySystemBackupResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    [Route("/gateways/{Id}/backup/systemdate", "GET")]
    public partial class GatewaySystemBackupDate
        : IReturn<GatewaySystemBackupDateResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewaySystemBackupDateResponse
        : ResponseBase
    {
        public virtual DateTimeOffset? LastDate { get; set; }
    }

    public partial class GatewaySystemBackupResponse
        : ResponseBase
    {
        public virtual string Name { get; set; }
        public virtual string DownloadURL { get; set; }
        public virtual string MD5 { get; set; }
    }

    [Route("/gateways/{Id}/thermostat/{ThermostatId}/link", "POST")]
    public partial class GatewayThermostatLink
        : IReturn<GatewayThermostatLinkResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ThermostatId", Description="ThermostatId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string ThermostatId { get; set; }

        public virtual bool Link { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayThermostatLinkResponse
        : ResponseBase
    {
        public virtual bool LinkSuccess { get; set; }
    }

    [Route("/gateways/{Id}/thermostat/{ThermostatId}/main", "POST")]
    public partial class GatewayThermostatMain
        : IReturn<GatewayThermostatMainResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ThermostatId", Description="ThermostatId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string ThermostatId { get; set; }

        [ApiMember(Name="UserId", Description="UserId", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string UserId { get; set; }
    }

    public partial class GatewayThermostatMainResponse
        : ResponseBase
    {
        public virtual bool MainSuccess { get; set; }
    }

    [Route("/gateways/{Id}/thermostat/{ThermostatId}/setpoint", "POST")]
    public partial class GatewayThermostatSetPoint
        : IReturn<GatewayThermostatSetPointResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ThermostatId", Description="ThermostatId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string ThermostatId { get; set; }

        public virtual double Value { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayThermostatSetPointResponse
        : ResponseBase
    {
        public virtual bool SetPointSuccess { get; set; }
    }

    [Route("/gateways/{Id}/thermostat/{SensorId}/toggle", "POST")]
    public partial class GatewayThermostatToggle
        : IReturn<GatewayThermostatToggleResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ThermostatId", Description="ThermostatId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string ThermostatId { get; set; }

        public virtual bool Value { get; set; }
        public virtual string UserId { get; set; }
    }

    public partial class GatewayThermostatToggleResponse
        : ResponseBase
    {
        public virtual bool ToggleSuccess { get; set; }
    }

    [Route("/gateways/{GatewayId}/block", "GET")]
    [Route("/gateways/{GatewayId}/block", "POST")]
    public partial class GatewayToggleBlockedStatus
        : IReturn<GatewayToggleBlockedStatusResponse>
    {
        [ApiMember(Name="GatewayId", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid GatewayId { get; set; }
    }

    public partial class GatewayToggleBlockedStatusResponse
        : ResponseBase
    {
        public virtual bool Blocked { get; set; }
    }

    [Route("/gateways/{GatewayId}/unblock", "POST")]
    public partial class GatewayToggleUnblockedStatus
        : IReturn<GatewayToggleUnblockedStatusResponse>
    {
        [ApiMember(Name="GatewayId", Description="Gateway Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual Guid GatewayId { get; set; }
    }

    public partial class GatewayToggleUnblockedStatusResponse
        : ResponseBase
    {
        public virtual bool Blocked { get; set; }
    }

    [Route("/gateways/{Id}/tunnel/close", "POST")]
    public partial class GatewayTunnelClose
        : IReturn<GatewayTunnelCloseResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayTunnelCloseResponse
        : ResponseBase
    {
        public virtual bool CloseSuccess { get; set; }
    }

    [Route("/gateways/{Id}/tunnel/open", "POST")]
    public partial class GatewayTunnelOpen
        : IReturn<GatewayTunnelOpenResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayTunnelOpenResponse
        : ResponseBase
    {
        public virtual bool OpenSuccess { get; set; }
    }

    [Route("/gateways/{Id}/unregister", "POST")]
    public partial class GatewayUnregister
        : IReturn<GatewayUnregisterResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class GatewayUnregisterResponse
        : ResponseBase
    {
        public virtual bool UnregisterSuccess { get; set; }
    }

    [Route("/gateways/{Id}/update", "POST")]
    public partial class GatewayUpdate
        : IReturn<GatewayUpdateResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    [Route("/gateways/{Id}/updatecamera", "POST")]
    public partial class GatewayUpdateCamera
        : IReturn<GatewayUpdateCameraResponse>
    {
        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Camera", Description="Camera", ParameterType="body", DataType="Camera", IsRequired=true, Verb="POST")]
        public virtual Camera Camera { get; set; }
    }

    public partial class GatewayUpdateCameraResponse
        : ResponseBase
    {
        public virtual bool UpdateSuccess { get; set; }
    }

    [Route("/gateways/{Id}/sensor/{SensorId}/updateProperty", "POST")]
    public partial class GatewayUpdateProperties
        : IReturn<GatewayUpdatePropertiesResponse>
    {
        public GatewayUpdateProperties()
        {
            Properties = new Dictionary<string, Object>{};
        }

        [ApiMember(Name="Id", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="SensorId", Description="GatewayId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        public virtual Dictionary<string, Object> Properties { get; set; }
    }

    public partial class GatewayUpdatePropertiesResponse
        : ResponseBase
    {
        public virtual bool UpdateSuccess { get; set; }
    }

    public partial class GatewayUpdateResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/update/status", "POST")]
    public partial class GatewayUpdateStatus
        : IReturn<GatewayUpdateStatusResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="UpdateStatus", Description="Update Status", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string UpdateStatus { get; set; }
    }

    public partial class GatewayUpdateStatusResponse
        : ResponseBase
    {
    }

    [Route("/gateways/{Id}/upnpdevicesinfo", "GET")]
    public partial class GatewayUPnPDevicesInfo
        : IReturn<GatewayUPnPDevicesInfoResponse>
    {
        [ApiMember(Name="Id", Description="Id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }
    }

    public partial class GatewayUPnPDevicesInfoResponse
        : ResponseBase
    {
        public GatewayUPnPDevicesInfoResponse()
        {
            DevicesInfo = new List<UPnPDeviceInfo>{};
        }

        public virtual List<UPnPDeviceInfo> DevicesInfo { get; set; }
    }

    [Route("/gateways/{Id}/Sensors/{SensorId}/writeregister", "POST")]
    public partial class GatewayWriteRegister
        : IReturn<GatewayWriteRegisterResponse>
    {
        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="RegConfig", Description="Register number to write", ParameterType="body", DataType="ZWaveConfiguration", IsRequired=true)]
        public virtual ZWaveRegister RegConfig { get; set; }
    }

    public partial class GatewayWriteRegisterResponse
        : ResponseBase
    {
        public virtual bool WriteRegisterSuccess { get; set; }
    }

    [Route("/gateways/{Id}/Sensors/{SensorId}/writeregisters", "POST")]
    public partial class GatewayWriteRegisters
        : IReturn<GatewayWriteRegistersResponse>
    {
        public GatewayWriteRegisters()
        {
            RegConfig = new List<ZWaveRegister>{};
        }

        [ApiMember(Name="Id", Description="Gateway Id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="RegConfig", Description="Register numbers to write", ParameterType="body", DataType="List<ZWaveConfiguration>", IsRequired=true)]
        public virtual List<ZWaveRegister> RegConfig { get; set; }
    }

    public partial class GatewayWriteRegistersResponse
        : ResponseBase
    {
        public virtual bool WriteRegistersSuccess { get; set; }
    }

    public partial class GWVersionResponse
        : ResponseBase
    {
        public virtual bool VersionSuccess { get; set; }
    }

    [Route("/installations/{Id}/activate", "POST")]
    public partial class InstallationActivate
        : IReturn<InstallationActivateResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Name", Description="Installation Name", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string Name { get; set; }
    }

    public partial class InstallationActivateResponse
        : ResponseBase
    {
        public virtual bool Activated { get; set; }
    }

    [Route("/installations/{Id}/activeSchedules", "GET")]
    [Route("/installations/{Id}/activeSchedules", "PATCH")]
    public partial class InstallationActiveSchedules
        : IReturn<InstallationActiveSchedulesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual string Type { get; set; }
        public virtual int ActiveSchedules { get; set; }
    }

    public partial class InstallationActiveSchedulesResponse
        : ResponseBase
    {
        public virtual int ActiveSchedules { get; set; }
    }

    [Route("/installations/{Id}/addcamera", "POST")]
    public partial class InstallationAddCamera
        : IReturn<InstallationAddCameraResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual string IPAddress { get; set; }
        public virtual int Port { get; set; }
        public virtual string Model { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string VideoURL { get; set; }
        public virtual string VideoFormat { get; set; }
        public virtual string VideoProtocol { get; set; }
        public virtual string RtspPort { get; set; }
        public virtual string PictureURL { get; set; }
        public virtual string PictureProtocol { get; set; }
        public virtual string PictureFormat { get; set; }
        public virtual string ZoomInURL { get; set; }
        public virtual string ZoomOutURL { get; set; }
        public virtual string PanLeftURL { get; set; }
        public virtual string PanRightURL { get; set; }
        public virtual string TiltUpURL { get; set; }
        public virtual string TiltDownURL { get; set; }
        public virtual string PTZStopURL { get; set; }
        public virtual string Authentication { get; set; }
        public virtual string User { get; set; }
        public virtual string Password { get; set; }
    }

    public partial class InstallationAddCameraResponse
        : ResponseBase
    {
        public virtual Camera NewCamera { get; set; }
    }

    [Route("/installations/{Id}/addorupdatesensors", "POST")]
    public partial class InstallationAddOrUpdateSensors
        : IReturn<InstallationAddOrUpdateSensorsResponse>
    {
        public InstallationAddOrUpdateSensors()
        {
            SensorAreas = new List<SensorArea>{};
        }

        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorAreas", Description="Sensors + Area name", ParameterType="body", DataType="List<SensorArea>", IsRequired=true)]
        public virtual List<SensorArea> SensorAreas { get; set; }
    }

    public partial class InstallationAddOrUpdateSensorsResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/addwmbussensor", "POST")]
    public partial class InstallationAddWMBusSensor
        : IReturn<InstallationAddWMBusSensorResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Manufacturer { get; set; }
        public virtual string Model { get; set; }
        public virtual string Version { get; set; }
        public virtual string SerialId { get; set; }
        public virtual string Key { get; set; }
        public virtual double? AccumulatedScale { get; set; }
        public virtual double? InstantScale { get; set; }
    }

    public partial class InstallationAddWMBusSensorResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/areas", "GET")]
    public partial class InstallationAreas
        : IReturn<InstallationAreasResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationAreasResponse
        : ResponseBase
    {
        public InstallationAreasResponse()
        {
            Areas = new List<Area>{};
        }

        public virtual List<Area> Areas { get; set; }
    }

    [Route("/installations/{id}/users/assignable", "GET")]
    public partial class InstallationAssignableUsers
        : IReturn<InstallationAssignableUsersResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationAssignableUsersResponse
        : ResponseBase
    {
        public InstallationAssignableUsersResponse()
        {
            Users = new List<AssignableUser>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<AssignableUser> Users { get; set; }
    }

    [Route("/installations/{Id}/cameras", "GET")]
    public partial class InstallationCameras
        : IReturn<InstallationCamerasResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationCamerasResponse
        : ResponseBase
    {
        public InstallationCamerasResponse()
        {
            Cameras = new List<Camera>{};
        }

        public virtual List<Camera> Cameras { get; set; }
    }

    [Route("/installations/{Id}/city", "PATCH")]
    public partial class InstallationCity
        : IReturn<InstallationCityResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Woeid", Description="City Woeid", ParameterType="body", DataType="int", IsRequired=true, Verb="PATCH")]
        public virtual int Woeid { get; set; }
    }

    public partial class InstallationCityResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/comfort", "GET")]
    public partial class InstallationComfort
        : IReturn<InstallationComfortResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    [Route("/installations/{Id}/areas/comfort", "GET")]
    public partial class InstallationComfortAreas
        : IReturn<InstallationComfortAreasResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationComfortAreasResponse
        : ResponseBase
    {
        public InstallationComfortAreasResponse()
        {
            ComfortAreas = new List<ComfortArea>{};
        }

        public virtual List<ComfortArea> ComfortAreas { get; set; }
    }

    [Route("/installations/{Id}/comfortmonthvalues", "GET")]
    public partial class InstallationComfortMonthValues
        : IReturn<InstallationComfortMonthValuesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationComfortMonthValuesResponse
        : ResponseBase
    {
        public InstallationComfortMonthValuesResponse()
        {
            ComfortMonthValues = new List<ComfortMonthValues>{};
        }

        public virtual List<ComfortMonthValues> ComfortMonthValues { get; set; }
    }

    public partial class InstallationComfortResponse
        : ResponseBase
    {
        public virtual ComfortValues ComfortValues { get; set; }
    }

    [Route("/installations/{Id}/comfortStatus", "GET")]
    [Route("/installations/{Id}/comfortStatus", "PATCH")]
    public partial class InstallationComfortStatus
        : IReturn<InstallationComfortStatusResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="ComfortStatus", Description="ComfortStatus", ParameterType="body", DataType="string", IsRequired=true, Verb="PATCH")]
        public virtual string ComfortStatus { get; set; }
    }

    public partial class InstallationComfortStatusResponse
        : ResponseBase
    {
        public virtual string ComfortStatus { get; set; }
    }

    [Route("/installations/{Id}/consumption/summary", "GET")]
    public partial class InstallationConsumptionSummary
        : IReturn<InstallationConsumptionSummaryResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Consumption type", ParameterType="query", DataType="string", IsRequired=true)]
        public virtual string Type { get; set; }

        [ApiMember(Name="UserTimeZone", Description="User time zone", ParameterType="query", DataType="string", IsRequired=true)]
        public virtual string UserTimeZone { get; set; }
    }

    public partial class InstallationConsumptionSummaryResponse
        : ResponseBase
    {
        public virtual double DayProduction { get; set; }
        public virtual double DayConsumption { get; set; }
        public virtual double DayProductionPrediction { get; set; }
        public virtual double DayConsumptionPrediction { get; set; }
        public virtual double WeekProduction { get; set; }
        public virtual double WeekConsumption { get; set; }
        public virtual double WeekProductionPrediction { get; set; }
        public virtual double WeekConsumptionPrediction { get; set; }
        public virtual double MonthProduction { get; set; }
        public virtual double MonthConsumption { get; set; }
        public virtual double MonthProductionPrediction { get; set; }
        public virtual double MonthConsumptionPrediction { get; set; }
        public virtual double? PricekWh { get; set; }
        public virtual double? PriceGaskWh { get; set; }
        public virtual double? PriceHeatingkWh { get; set; }
        public virtual double? PriceWaterM3 { get; set; }
        public virtual double? GasMeterTokWh { get; set; }
        public virtual string MasterSensorId { get; set; }
        public virtual string ProductionSensorId { get; set; }
    }

    [Route("/installations/{Id}/energy/consumption/values", "GET")]
    public partial class InstallationConsumptionValues
        : IReturn<InstallationConsumptionValuesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="UserTimeZone", Description="UserTimeZone", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserTimeZone { get; set; }

        [ApiMember(Name="ConsumptionPeriod", Description="ConsumptionPeriod", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string ConsumptionPeriod { get; set; }
    }

    public partial class InstallationConsumptionValuesResponse
        : ResponseBase
    {
        public InstallationConsumptionValuesResponse()
        {
            Consumptions = new List<InstallationCategoryPeriodConsumption>{};
        }

        public virtual List<InstallationCategoryPeriodConsumption> Consumptions { get; set; }
    }

    [Route("/installations/{Id}/backup/create", "POST")]
    public partial class InstallationCreateBackup
        : IReturn<InstallationCreateBackupResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationCreateBackupResponse
        : ResponseBase
    {
        public virtual bool CreateBackupSuccess { get; set; }
    }

    [Route("/installations/{Id}/backup/createsystem", "POST")]
    public partial class InstallationCreateSystemBackup
        : IReturn<InstallationCreateSystemBackupResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationCreateSystemBackupResponse
        : ResponseBase
    {
        public virtual bool CreateSystemBackupSuccess { get; set; }
    }

    [Route("/installations/{Id}/datacloudstorage", "GET")]
    [Route("/installations/{Id}/datacloudstorage", "POST")]
    public partial class InstallationDataCloudStorage
        : IReturn<InstallationDataCloudStorageResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="Interval", Description="Interval aggregation time in minutes (15 every 15 mins, 30 every half an hour, 60 every hour...)", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int Interval { get; set; }

        [ApiMember(Name="AggregationType", Description="Type of aggregation by interval ('avg' or 'sum')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string AggregationType { get; set; }

        [ApiMember(Name="Installation", Description="Installation data", ParameterType="body", DataType="Installation", IsRequired=true, Verb="POST")]
        public virtual Installation Installation { get; set; }
    }

    public partial class InstallationDataCloudStorageResponse
        : ResponseBase
    {
        public InstallationDataCloudStorageResponse()
        {
            Values = new List<InstallationDataPoint>{};
        }

        public virtual List<InstallationDataPoint> Values { get; set; }
    }

    [Route("/installations/{Id}/dataupdater", "PATCH")]
    public partial class InstallationDataUpdater
        : IReturn<InstallationDataUpdaterResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual double? AverageTemperature { get; set; }
        public virtual double? AverageHumidity { get; set; }
    }

    public partial class InstallationDataUpdaterResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/demandresponse", "POST")]
    public partial class InstallationDemandResponse
        : IReturn<InstallationDemandResponseResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual string Type { get; set; }
        public virtual string EnergySource { get; set; }
        public virtual string FromDate { get; set; }
        public virtual string ToDate { get; set; }
        public virtual string Action { get; set; }
        public virtual int ActionValue { get; set; }
        public virtual string ActionOperation { get; set; }
        public virtual int Reward { get; set; }
        public virtual string Tips { get; set; }
        public virtual string DRId { get; set; }
    }

    public partial class InstallationDemandResponseResponse
        : ResponseBase
    {
        public virtual Guid DemandResponseId { get; set; }
    }

    [Route("/installations/{Id}/details", "GET")]
    [Route("/installations/{Id}", "DELETE")]
    public partial class InstallationDetails
        : IReturn<InstallationDetailsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationDetailsResponse
        : ResponseBase
    {
        public virtual Installation Installation { get; set; }
    }

    [Route("/installations/{Id}/devices/area", "GET")]
    public partial class InstallationDeviceAndArea
        : IReturn<InstallationDeviceAndAreaResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationDeviceAndAreaResponse
        : ResponseBase
    {
        public InstallationDeviceAndAreaResponse()
        {
            DeviceAreas = new List<DeviceArea>{};
        }

        public virtual List<DeviceArea> DeviceAreas { get; set; }
    }

    [Route("/installations/{Id}/energy/consumption", "POST")]
    [Route("/installations/{Id}/energy/consumption", "GET")]
    public partial class InstallationEnergyConsumption
        : IReturn<InstallationEnergyConsumptionResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="InstallationCategoryConsumption", Description="InstallationCategoryConsumption", ParameterType="body", DataType="InstallationCategoryConsumption", IsRequired=true, Verb="POST")]
        public virtual InstallationCategoryConsumption InstallationCategoryConsumption { get; set; }
    }

    [Route("/installations/{Id}/energy/consumption/categorized/day", "GET")]
    public partial class InstallationEnergyConsumptionByCategoriesDay
        : IReturn<InstallationEnergyConsumptionByCategoriesDayResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="TimeZone", Description="TimeZone standard name", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string TimeZone { get; set; }
    }

    public partial class InstallationEnergyConsumptionByCategoriesDayResponse
        : ResponseBase
    {
        public InstallationEnergyConsumptionByCategoriesDayResponse()
        {
            TotalConsumption = new Dictionary<string, double>{};
            DistributedConsumption = new List<BarChartCategorizedItem>{};
        }

        public virtual Dictionary<string, double> TotalConsumption { get; set; }
        public virtual List<BarChartCategorizedItem> DistributedConsumption { get; set; }
    }

    [Route("/installations/{Id}/energy/consumption/categorized/month", "GET")]
    public partial class InstallationEnergyConsumptionByCategoriesMonth
        : IReturn<InstallationEnergyConsumptionByCategoriesMonthResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="TimeZone", Description="TimeZone standard name", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string TimeZone { get; set; }
    }

    public partial class InstallationEnergyConsumptionByCategoriesMonthResponse
        : ResponseBase
    {
        public InstallationEnergyConsumptionByCategoriesMonthResponse()
        {
            TotalConsumption = new Dictionary<string, double>{};
            DistributedConsumption = new List<BarChartCategorizedItem>{};
        }

        public virtual Dictionary<string, double> TotalConsumption { get; set; }
        public virtual List<BarChartCategorizedItem> DistributedConsumption { get; set; }
    }

    [Route("/installations/{Id}/energy/consumption/categorized/week", "GET")]
    public partial class InstallationEnergyConsumptionByCategoriesWeek
        : IReturn<InstallationEnergyConsumptionByCategoriesWeekResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="TimeZone", Description="TimeZone standard name", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string TimeZone { get; set; }
    }

    public partial class InstallationEnergyConsumptionByCategoriesWeekResponse
        : ResponseBase
    {
        public InstallationEnergyConsumptionByCategoriesWeekResponse()
        {
            TotalConsumption = new Dictionary<string, double>{};
            DistributedConsumption = new List<BarChartCategorizedItem>{};
        }

        public virtual Dictionary<string, double> TotalConsumption { get; set; }
        public virtual List<BarChartCategorizedItem> DistributedConsumption { get; set; }
    }

    public partial class InstallationEnergyConsumptionResponse
        : ResponseBase
    {
        public InstallationEnergyConsumptionResponse()
        {
            CategoryConsumptions = new List<InstallationCategoryConsumption>{};
        }

        public virtual List<InstallationCategoryConsumption> CategoryConsumptions { get; set; }
    }

    [Route("/installations/{Id}/energy/trend/{period}", "GET")]
    public partial class InstallationEnergyTrend
        : IReturn<InstallationEnergyTrendResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Period", Description="Trend period", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Period { get; set; }
    }

    public partial class InstallationEnergyTrendResponse
        : ResponseBase
    {
        public virtual double? EnergyTrend { get; set; }
    }

    [Route("/installations/{Id}/energy/trend", "GET")]
    [Route("/installations/{Id}/energy/trend", "PATCH")]
    public partial class InstallationEnergyTrends
        : IReturn<InstallationEnergyTrendsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual double DayEnergyTrendValue { get; set; }
        public virtual double WeekEnergyTrendValue { get; set; }
        public virtual double MonthEnergyTrendValue { get; set; }
        public virtual string DayEnergyTrendCode { get; set; }
        public virtual string WeekEnergyTrendCode { get; set; }
        public virtual string MonthEnergyTrendCode { get; set; }
        public virtual DateTime EnergyTrendDate { get; set; }
    }

    public partial class InstallationEnergyTrendsResponse
        : ResponseBase
    {
        public virtual double? DayEnergyTrendValue { get; set; }
        public virtual string DayEnergyTrendCode { get; set; }
        public virtual double? WeekEnergyTrendValue { get; set; }
        public virtual string WeekEnergyTrendCode { get; set; }
        public virtual double? MonthEnergyTrendValue { get; set; }
        public virtual string MonthEnergyTrendCode { get; set; }
    }

    [Route("/installations/{Id}/energytrend/summary", "GET")]
    public partial class InstallationEnergyTrendSummary
        : IReturn<InstallationEnergyTrendSummaryResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ConsumptionPeriod", Description="ConsumptionPeriod", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string ConsumptionPeriod { get; set; }
    }

    public partial class InstallationEnergyTrendSummaryResponse
        : ResponseBase
    {
        public virtual string EnergyTrend { get; set; }
        public virtual double? EnergyValue { get; set; }
    }

    [Route("/installations/{Id}/floorplans", "GET")]
    public partial class InstallationFloorplans
        : IReturn<InstallationFloorplansResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationFloorplansResponse
        : ResponseBase
    {
        public InstallationFloorplansResponse()
        {
            Floorplans = new List<Floorplan>{};
        }

        public virtual List<Floorplan> Floorplans { get; set; }
    }

    [Route("/installations/{id}/floorplans/batch", "GET")]
    public partial class InstallationFloorplansWithAreasBatch
        : IReturn<InstallationFloorplansWithAreasBatchResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string SensorId { get; set; }
    }

    public partial class InstallationFloorplansWithAreasBatchResponse
        : ResponseBase
    {
        public InstallationFloorplansWithAreasBatchResponse()
        {
            FloorPlansWithAreas = new List<FloorPlanWithAreas>{};
        }

        public virtual List<FloorPlanWithAreas> FloorPlansWithAreas { get; set; }
    }

    [Route("/installations/{Id}/gas/trend", "GET")]
    [Route("/installations/{Id}/gas/trend", "PATCH")]
    public partial class InstallationGasTrends
        : IReturn<InstallationGasTrendsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual double DayGasTrendValue { get; set; }
        public virtual double WeekGasTrendValue { get; set; }
        public virtual double MonthGasTrendValue { get; set; }
        public virtual string DayGasTrendCode { get; set; }
        public virtual string WeekGasTrendCode { get; set; }
        public virtual string MonthGasTrendCode { get; set; }
        public virtual DateTime GasTrendDate { get; set; }
    }

    public partial class InstallationGasTrendsResponse
        : ResponseBase
    {
        public virtual double? DayGasTrendValue { get; set; }
        public virtual string DayGasTrendCode { get; set; }
        public virtual double? WeekGasTrendValue { get; set; }
        public virtual string WeekGasTrendCode { get; set; }
        public virtual double? MonthGasTrendValue { get; set; }
        public virtual string MonthGasTrendCode { get; set; }
    }

    [Route("/installations/{Id}/gatewaySecurityStatus", "PUT")]
    public partial class InstallationGatewaySecurityStatus
        : IReturn<InstallationGatewaySecurityStatusResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SecurityStatus", Description="SecurityStatus", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string SecurityStatus { get; set; }
    }

    public partial class InstallationGatewaySecurityStatusResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/heating/trend", "GET")]
    [Route("/installations/{Id}/heating/trend", "PATCH")]
    public partial class InstallationHeatingTrends
        : IReturn<InstallationHeatingTrendsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual double DayHeatingTrendValue { get; set; }
        public virtual double WeekHeatingTrendValue { get; set; }
        public virtual double MonthHeatingTrendValue { get; set; }
        public virtual string DayHeatingTrendCode { get; set; }
        public virtual string WeekHeatingTrendCode { get; set; }
        public virtual string MonthHeatingTrendCode { get; set; }
        public virtual DateTime HeatingTrendDate { get; set; }
    }

    public partial class InstallationHeatingTrendsResponse
        : ResponseBase
    {
        public virtual double? DayHeatingTrendValue { get; set; }
        public virtual string DayHeatingTrendCode { get; set; }
        public virtual double? WeekHeatingTrendValue { get; set; }
        public virtual string WeekHeatingTrendCode { get; set; }
        public virtual double? MonthHeatingTrendValue { get; set; }
        public virtual string MonthHeatingTrendCode { get; set; }
    }

    [Route("/installations/{Id}/trees/lastmonths/{MonthNum}", "GET")]
    public partial class InstallationLastMonthsTrees
        : IReturn<InstallationLastMonthsTreesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="MonthNum", Description="Number of months", ParameterType="path", DataType="int", IsRequired=true, Verb="GET")]
        public virtual int MonthNum { get; set; }

        [ApiMember(Name="Type", Description="Tree Type", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class InstallationLastMonthsTreesResponse
        : ResponseBase
    {
        public virtual IDictionary<string, int> Trees { get; set; }
    }

    [Route("/installations/{Id}/mainthermostat", "GET")]
    public partial class InstallationMainThermostat
        : IReturn<InstallationMainThermostatResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationMainThermostatResponse
        : ResponseBase
    {
        public virtual Sensor MainThermostat { get; set; }
    }

    [Route("/installations/{Id}/name", "PATCH")]
    public partial class InstallationName
        : IReturn<InstallationNameResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Name", Description="Installation Name", ParameterType="body", DataType="string", IsRequired=true, Verb="PATCH")]
        public virtual string Name { get; set; }
    }

    public partial class InstallationNameResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/nodes", "GET")]
    public partial class InstallationNodes
        : IReturn<InstallationNodesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationNodesResponse
        : ResponseBase
    {
        public InstallationNodesResponse()
        {
            Nodes = new List<Node>{};
        }

        public virtual List<Node> Nodes { get; set; }
    }

    [Route("/installations/{Id}/energy/oldconsumption/values", "GET")]
    public partial class InstallationOldConsumptionValues
        : IReturn<InstallationConsumptionValuesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="UserTimeZone", Description="UserTimeZone", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserTimeZone { get; set; }

        [ApiMember(Name="ConsumptionPeriod", Description="ConsumptionPeriod", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string ConsumptionPeriod { get; set; }
    }

    [Route("/installations/{Id}/overallhumidity", "PATCH")]
    public partial class InstallationOverallHumidity
        : IReturn<InstallationOverallHumidityResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="OverallHumidity", Description="New Average Humidity", ParameterType="body", DataType="double?", IsRequired=true)]
        public virtual double? OverallHumidity { get; set; }
    }

    public partial class InstallationOverallHumidityResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/overalltemp", "PATCH")]
    public partial class InstallationOverallTemp
        : IReturn<InstallationOverallTempResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="OverallTemp", Description="New Average Temperature", ParameterType="body", DataType="double?", IsRequired=true)]
        public virtual double? OverallTemp { get; set; }
    }

    public partial class InstallationOverallTempResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/energy/percentageconsumption/values", "GET")]
    public partial class InstallationPercentageConsumptionValues
        : IReturn<InstallationPercentageConsumptionValuesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="UserTimeZone", Description="UserTimeZone", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string UserTimeZone { get; set; }

        [ApiMember(Name="ConsumptionPeriod", Description="ConsumptionPeriod", ParameterType="query", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string ConsumptionPeriod { get; set; }
    }

    public partial class InstallationPercentageConsumptionValuesResponse
        : ResponseBase
    {
        public InstallationPercentageConsumptionValuesResponse()
        {
            ConsumptionPercentagesForPeriod = new Dictionary<string, double>{};
        }

        public virtual Dictionary<string, double> ConsumptionPercentagesForPeriod { get; set; }
    }

    [Route("/installations/{Id}/photos", "GET")]
    public partial class InstallationPhotos
        : IReturn<InstallationPhotosResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationPhotosResponse
        : ResponseBase
    {
        public InstallationPhotosResponse()
        {
            Cameras = new List<Camera>{};
        }

        public virtual List<Camera> Cameras { get; set; }
    }

    [Route("/installations/{Id}/predictions", "GET")]
    public partial class InstallationPredictions
        : IReturn<InstallationPredictionsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }
    }

    public partial class InstallationPredictionsResponse
        : ResponseBase
    {
        public InstallationPredictionsResponse()
        {
            InstallationValuePredictions = new List<PredictionPoint>{};
            SensorValuesPredictions = new List<List<PredictionPoint>>{};
            InstallationValues = new List<DataPoint>{};
            SensorValues = new List<List<DataPoint>>{};
        }

        public virtual List<PredictionPoint> InstallationValuePredictions { get; set; }
        public virtual List<List<PredictionPoint>> SensorValuesPredictions { get; set; }
        public virtual List<DataPoint> InstallationValues { get; set; }
        public virtual List<List<DataPoint>> SensorValues { get; set; }
        public virtual int NumSensors { get; set; }
    }

    [Route("/installations/{Id}/register", "POST")]
    public partial class InstallationRegister
        : IReturn<InstallationRegisterResponse>
    {
        [ApiMember(Name="Installation", Description="Installation details", ParameterType="body", DataType="Installation", IsRequired=true, Verb="POST")]
        public virtual Installation Installation { get; set; }
    }

    public partial class InstallationRegisterResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/sensors/values/live/request", "POST")]
    public partial class InstallationRequestLiveSensorValues
        : IReturn<InstallationRequestLiveSensorValuesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationRequestLiveSensorValuesResponse
        : ResponseBase
    {
    }

    [Route("/installations/{Id}/restart", "POST")]
    public partial class InstallationRestart
        : IReturn<InstallationRestartResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationRestartResponse
        : ResponseBase
    {
        public virtual bool RestartSuccess { get; set; }
    }

    [Route("/installations/{Id}/backup/restore", "POST")]
    public partial class InstallationRestoreBackup
        : IReturn<InstallationRestoreBackupResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationRestoreBackupResponse
        : ResponseBase
    {
        public virtual bool RestoreBackupSuccess { get; set; }
    }

    [Route("/installations/{Id}/backup/restoresystem", "POST")]
    public partial class InstallationRestoreSystemBackup
        : IReturn<InstallationRestoreSystemBackupResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationRestoreSystemBackupResponse
        : ResponseBase
    {
        public virtual bool RestoreSystemBackupSuccess { get; set; }
    }

    [Route("/installations", "POST")]
    [Route("/installations", "PUT")]
    public partial class Installations
        : IReturn<InstallationDetailsResponse>
    {
        [ApiMember(Name="details", Description="Installation details", ParameterType="body", DataType="Installation", IsRequired=true, Verb="POST")]
        [ApiMember(Name="details", Description="Installation details", ParameterType="body", DataType="Installation", IsRequired=true, Verb="PUT")]
        public virtual Installation Installation { get; set; }
    }

    [Route("/installations/andsensorandcategory", "GET")]
    public partial class InstallationsAndSensorAndCategory
        : IReturn<InstallationsAndSensorAndCategoryResponse>
    {
        [ApiMember(Name="SensorId", Description="Sensor id", ParameterType="body", DataType="string", Verb="GET")]
        public virtual string SensorId { get; set; }

        [ApiMember(Name="CategoryType", Description="CategoryType of the sensors to be retrieved", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string CategoryType { get; set; }
    }

    public partial class InstallationsAndSensorAndCategoryResponse
        : ResponseBase
    {
        public InstallationsAndSensorAndCategoryResponse()
        {
            InstallationsAndSensorAndCategory = new List<InstallationWithSensorAndCategory>{};
        }

        public virtual List<InstallationWithSensorAndCategory> InstallationsAndSensorAndCategory { get; set; }
    }

    [Route("/installations", "GET")]
    public partial class InstallationsBatch
        : IReturn<InstallationsBatchResponse>
    {
        [ApiMember(Name="WithType", Description="Gets installaions that have this type of sensors", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string WithType { get; set; }
    }

    public partial class InstallationsBatchResponse
        : ResponseBase
    {
        public InstallationsBatchResponse()
        {
            Installations = new List<Installation>{};
        }

        public virtual List<Installation> Installations { get; set; }
    }

    [Route("/installations/{Id}/securityStatus", "GET")]
    [Route("/installations/{Id}/securityStatus", "PUT")]
    public partial class InstallationSecurityStatus
        : IReturn<InstallationSecurityStatusResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual string SecurityStatus { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Trigger { get; set; }
    }

    public partial class InstallationSecurityStatusResponse
        : ResponseBase
    {
        public virtual string SecurityStatus { get; set; }
    }

    [Route("/installations/{Id}/sensors", "GET")]
    public partial class InstallationSensors
        : IReturn<InstallationSensorsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    [Route("/installations/{Id}/sensors/actuable", "GET")]
    public partial class InstallationSensorsActuable
        : IReturn<InstallationSensorsActuableResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationSensorsActuableResponse
        : ResponseBase
    {
        public InstallationSensorsActuableResponse()
        {
            Sensors = new List<SensorWithAreaName>{};
        }

        public virtual List<SensorWithAreaName> Sensors { get; set; }
    }

    [Route("/installations/{Id}/sensors/comfort", "GET")]
    public partial class InstallationSensorsComfort
        : IReturn<InstallationSensorsComfortResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationSensorsComfortResponse
        : ResponseBase
    {
        public InstallationSensorsComfortResponse()
        {
            Sensors = new List<SensorWithAreaName>{};
        }

        public virtual List<SensorWithAreaName> Sensors { get; set; }
    }

    [Route("/installations/{Id}/sensors/comfort/scheduler", "GET")]
    public partial class InstallationSensorsComfortScheduler
        : IReturn<InstallationSensorsComfortResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationSensorsResponse
        : ResponseBase
    {
        public InstallationSensorsResponse()
        {
            Sensors = new List<Sensor>{};
        }

        public virtual List<Sensor> Sensors { get; set; }
        public virtual string InstallationId { get; set; }
    }

    [Route("/installations/{Id}/sensors/security", "GET")]
    public partial class InstallationSensorsSecurity
        : IReturn<InstallationSensorsSecurityResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationSensorsSecurityResponse
        : ResponseBase
    {
        public InstallationSensorsSecurityResponse()
        {
            Sensors = new List<SensorWithAreaName>{};
        }

        public virtual List<SensorWithAreaName> Sensors { get; set; }
    }

    [Route("/installations/{Id}/sensors/summary", "GET")]
    public partial class InstallationSensorsSummary
        : IReturn<InstallationSensorsSummaryResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }
    }

    public partial class InstallationSensorsSummaryResponse
        : ResponseBase
    {
        public InstallationSensorsSummaryResponse()
        {
            Sensors = new List<SensorBaseWithAreaIDandName>{};
        }

        public virtual List<SensorBaseWithAreaIDandName> Sensors { get; set; }
    }

    [Route("/installations/{Id}/sensors/area", "GET")]
    public partial class InstallationSensorsWithAreaIdAndName
        : IReturn<InstallationSensorsWithAreaIdAndNameResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Protocol", Description="Protocol name", ParameterType="query", DataType="SensorProtocolType?")]
        public virtual SensorProtocolType? Protocol { get; set; }
    }

    public partial class InstallationSensorsWithAreaIdAndNameResponse
        : ResponseBase
    {
        public InstallationSensorsWithAreaIdAndNameResponse()
        {
            Sensors = new List<SensorWithAreaIdAndName>{};
        }

        public virtual List<SensorWithAreaIdAndName> Sensors { get; set; }
    }

    [Route("/installations/{Id}/sensors/values/live", "POST")]
    public partial class InstallationSensorValuesBatch
        : IReturn<SensorValuesBatchResponse>
    {
        public InstallationSensorValuesBatch()
        {
            SensorDataPoints = new List<SensorDataPoint>{};
        }

        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SensorDataPoints", Description="Collection of datapoints to insert", ParameterType="body", DataType="List<SensorDataPoint>", IsRequired=true, Verb="POST")]
        public virtual List<SensorDataPoint> SensorDataPoints { get; set; }
    }

    [Route("/installations/{Id}/shutdown", "POST")]
    public partial class InstallationShutdown
        : IReturn<InstallationShutdownResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationShutdownResponse
        : ResponseBase
    {
        public virtual bool ShutdownSuccess { get; set; }
    }

    [Route("/installations", "PATCH")]
    public partial class InstallationsPatch
        : QueryBase<Installation>, IReturn<QueryResponse<Installation>>
    {
        public InstallationsPatch()
        {
            Fields = new string[]{};
        }

        [ApiMember(Name="installation", Description="Intallation object", ParameterType="body", DataType="Installation", IsRequired=true, Verb="PATCH")]
        public virtual Installation Installation { get; set; }

        [ApiMember(Name="fields", Description="Fields to update", ParameterType="query", DataType="string[]", IsRequired=true, Verb="PATCH")]
        public virtual string[] Fields { get; set; }
    }

    [Route("/installations/{Id}/stats", "GET")]
    [Route("/installations/{Id}/stats", "POST")]
    public partial class InstallationsStats
        : IReturn<InstallationsStatsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET", ExcludeInSchema=true)]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET", ExcludeInSchema=true)]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET", ExcludeInSchema=true)]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET", ExcludeInSchema=true)]
        public virtual int PageSize { get; set; }

        public virtual DateTime Date { get; set; }
        public virtual double Uptime { get; set; }
    }

    public partial class InstallationsStatsResponse
        : ResponseBase
    {
        public InstallationsStatsResponse()
        {
            InstallationStats = new List<InstallationStats>{};
        }

        public virtual List<InstallationStats> InstallationStats { get; set; }
    }

    [Route("/installations/{Id}/users", "PUT")]
    public partial class InstallationsUsersBatch
        : IReturn<InstallationsUsersBatchResponse>
    {
        public InstallationsUsersBatch()
        {
            Users = new List<AssignableUser>{};
        }

        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Users", Description="Users", ParameterType="body", DataType="List<AssignableUser>", IsRequired=true)]
        public virtual List<AssignableUser> Users { get; set; }
    }

    public partial class InstallationsUsersBatchResponse
        : ResponseBase
    {
    }

    [Route("/installations/withmaster", "GET")]
    public partial class InstallationsWithMaster
        : IReturn<InstallationsWithMasterResponse>
    {
        [ApiMember(Name="MasterType", Description="Master category type", ParameterType="query", DataType="MasterConsumptionCategoryType", IsRequired=true, Verb="GET")]
        public virtual MasterConsumptionCategoryType MasterType { get; set; }
    }

    public partial class InstallationsWithMasterResponse
        : ResponseBase
    {
        public InstallationsWithMasterResponse()
        {
            Installations = new List<Installation>{};
        }

        public virtual List<Installation> Installations { get; set; }
    }

    [Route("/installations/{Id}/backup/systemdate", "GET")]
    public partial class InstallationSystemBackupDate
        : IReturn<InstallationSystemBackupDateResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class InstallationSystemBackupDateResponse
        : ResponseBase
    {
        public virtual DateTimeOffset? LastDate { get; set; }
    }

    [Route("/installations/{Id}/trees", "GET")]
    public partial class InstallationTrees
        : IReturn<InstallationTreesResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Type", Description="Tree Type", ParameterType="query", DataType="int", IsRequired=true, Verb="GET")]
        public virtual string Type { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="DateTime", Verb="GET")]
        public virtual DateTime FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual DateTime ToDate { get; set; }
    }

    public partial class InstallationTreesResponse
        : ResponseBase
    {
        public InstallationTreesResponse()
        {
            Trees = new List<DateTime>{};
        }

        public virtual List<DateTime> Trees { get; set; }
    }

    [Route("/installations/{Id}/users/", "GET")]
    public partial class InstallationUsers
        : IReturn<InstallationUsersResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual Guid Id { get; set; }

        [ApiMember(Name="Role", Description="Role", ParameterType="query", DataType="string")]
        public virtual string Role { get; set; }
    }

    public partial class InstallationUsersResponse
        : ResponseBase
    {
        public InstallationUsersResponse()
        {
            Users = new List<UserAuth>{};
        }

        public virtual Guid Id { get; set; }
        public virtual List<UserAuth> Users { get; set; }
    }

    [Route("/installations/{Id}/water/trend", "GET")]
    [Route("/installations/{Id}/water/trend", "PATCH")]
    public partial class InstallationWaterTrends
        : IReturn<InstallationWaterTrendsResponse>
    {
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="Installation id", ParameterType="path", DataType="Guid", IsRequired=true, Verb="PATCH", ExcludeInSchema=true)]
        public virtual Guid Id { get; set; }

        public virtual double DayWaterTrendValue { get; set; }
        public virtual double WeekWaterTrendValue { get; set; }
        public virtual double MonthWaterTrendValue { get; set; }
        public virtual string DayWaterTrendCode { get; set; }
        public virtual string WeekWaterTrendCode { get; set; }
        public virtual string MonthWaterTrendCode { get; set; }
        public virtual DateTime WaterTrendDate { get; set; }
    }

    public partial class InstallationWaterTrendsResponse
        : ResponseBase
    {
        public virtual double? DayWaterTrendValue { get; set; }
        public virtual string DayWaterTrendCode { get; set; }
        public virtual double? WeekWaterTrendValue { get; set; }
        public virtual string WeekWaterTrendCode { get; set; }
        public virtual double? MonthWaterTrendValue { get; set; }
        public virtual string MonthWaterTrendCode { get; set; }
    }

    public partial class ResponseBase
    {
        public virtual ResponseStatus ResponseStatus { get; set; }
    }

    [Route("/sensors/{Id}/areaName", "GET")]
    public partial class SensorAreaName
        : IReturn<SensorAreaNameResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorAreaNameResponse
        : ResponseBase
    {
        public virtual string SensorId { get; set; }
        public virtual string AreaName { get; set; }
    }

    [Route("/sensors/{Id}/area", "GET")]
    public partial class SensorAreaRequest
        : IReturn<SensorAreaResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorAreaResponse
        : ResponseBase
    {
        public virtual Area Area { get; set; }
    }

    [Route("/sensors/{id}/areas", "PUT")]
    public partial class SensorAreas
        : IReturn<SensorAreasResponse>
    {
        public SensorAreas()
        {
            Areas = new List<AssignableArea>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual Guid InstallationId { get; set; }
        public virtual List<AssignableArea> Areas { get; set; }
    }

    public partial class SensorAreasResponse
        : ResponseBase
    {
        public virtual bool IsAssigned { get; set; }
    }

    [Route("/sensors/{Id}/installations/assignable", "GET")]
    public partial class SensorAssignableInstallations
        : IReturn<AssignableInstallationsResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    [Route("/sensors/{Id}/batterylevel", "PATCH")]
    public partial class SensorBatteryLevel
        : IReturn<SensorBatteryLevelResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="BatteryLevelReport", Description="Battery Level Report", ParameterType="body", DataType="BatteryLevelReport", IsRequired=true)]
        public virtual BatteryLevelReport Report { get; set; }
    }

    [Route("/sensors/batterylevel", "PATCH")]
    public partial class SensorBatteryLevelBatch
        : IReturn<SensorBatteryLevelBatchResponse>
    {
        public SensorBatteryLevelBatch()
        {
            Reports = new List<BatteryLevelReport>{};
        }

        [ApiMember(Name="BatteryLevelReport", Description="Battery Level Reports", ParameterType="body", DataType="List<BatteryLevelReport>", IsRequired=true)]
        public virtual List<BatteryLevelReport> Reports { get; set; }
    }

    public partial class SensorBatteryLevelBatchResponse
        : ResponseBase
    {
    }

    public partial class SensorBatteryLevelResponse
        : ResponseBase
    {
    }

    [Route("/sensors/{Id}/bypass", "POST")]
    public partial class SensorBypass
        : IReturn<SensorBypassResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string InstallationId { get; set; }
        public virtual bool BypassStatus { get; set; }
    }

    public partial class SensorBypassResponse
        : ResponseBase
    {
        public virtual CommandResponseWrapper<bool> BypassResponse { get; set; }
    }

    [Route("/sensors/{Id}/children", "GET")]
    public partial class SensorChildren
        : IReturn<SensorChildrenResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorChildrenResponse
        : ResponseBase
    {
        public SensorChildrenResponse()
        {
            Children = new List<Sensor>{};
        }

        public virtual List<Sensor> Children { get; set; }
    }

    [Route("/sensors/{Id}/configurationupdate", "POST")]
    public partial class SensorConfigurationUpdate
        : IReturn<SensorConfigurationUpdateResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="DefaultSensorConfiguration", Description="DefaultSensorConfiguration", ParameterType="body", DataType="DefaultSensorConfiguration", IsRequired=true)]
        public virtual DefaultSensorConfiguration DefaultSensorConfiguration { get; set; }
    }

    public partial class SensorConfigurationUpdateResponse
        : ResponseBase
    {
        public virtual bool ConfigurationSuccess { get; set; }
    }

    [Route("/sensors/{Id}/configure", "POST")]
    public partial class SensorConfigure
        : IReturn<SensorConfigureResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string InstallationId { get; set; }
        public virtual string Configuration { get; set; }
    }

    public partial class SensorConfigureResponse
        : ResponseBase
    {
        public virtual bool ConfigurationSuccess { get; set; }
    }

    [Route("/sensors/copyvalues", "POST")]
    public partial class SensorCopyValues
        : IReturn<SensorCopyValuesResponse>
    {
        public virtual string SourceSensorId { get; set; }
        public virtual string TargetSensorId { get; set; }
        public virtual string Key { get; set; }
        public virtual DateTime From { get; set; }
        public virtual DateTime To { get; set; }
    }

    [Route("/sensors/copyvalues/active", "GET")]
    public partial class SensorCopyValuesActive
        : IReturn<SensorCopyValuesBatchResponse>
    {
        [ApiMember(Name="InstallationId", Description="Installation id", ParameterType="query", DataType="Guid", IsRequired=true)]
        public virtual Guid InstallationId { get; set; }
    }

    public partial class SensorCopyValuesBatchResponse
        : ResponseBase
    {
        public SensorCopyValuesBatchResponse()
        {
            ActiveCopyValuesOperations = new List<SensorCopyValues>{};
        }

        public virtual List<SensorCopyValues> ActiveCopyValuesOperations { get; set; }
    }

    [Route("/sensors/copyvalues/complete", "POST")]
    public partial class SensorCopyValuesComplete
        : IReturn<SensorCopyValuesCompleteResponse>
    {
        public virtual string SourceSensorId { get; set; }
        public virtual string TargetSensorId { get; set; }
        public virtual string FromTicks { get; set; }
        public virtual string ToTicks { get; set; }
    }

    public partial class SensorCopyValuesCompleteResponse
        : ResponseBase
    {
    }

    public partial class SensorCopyValuesResponse
        : ResponseBase
    {
    }

    [Route("/sensors/copyvalues/start", "POST")]
    public partial class SensorCopyValuesStart
        : IReturn<SensorCopyValuesStartResponse>
    {
        public virtual string SourceSensorId { get; set; }
        public virtual string TargetSensorId { get; set; }
        public virtual string FromTicks { get; set; }
        public virtual string ToTicks { get; set; }
    }

    public partial class SensorCopyValuesStartResponse
        : ResponseBase
    {
    }

    [Route("/sensors/{Id}/details", "GET")]
    [Route("/sensors/{Id}", "DELETE")]
    public partial class SensorDetails
        : IReturn<SensorDetailsResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorDetailsResponse
        : ResponseBase
    {
        public virtual Sensor Sensor { get; set; }
    }

    [Route("/sensors/{Id}/dimmable", "POST")]
    public partial class SensorDimmable
        : IReturn<SensorDimmableResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual double Value { get; set; }
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorDimmableResponse
        : ResponseBase
    {
        public virtual bool CommunicationSuccess { get; set; }
        public virtual bool DimmerSuccess { get; set; }
    }

    [Route("/sensors/{Id}/doorlocktoggle", "POST")]
    public partial class SensorDoorLockToggle
        : IReturn<SensorDoorLockToggleResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual bool Lock { get; set; }
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorDoorLockToggleResponse
        : ResponseBase
    {
        public virtual bool CommunicationSuccess { get; set; }
        public virtual bool ToggleSuccess { get; set; }
    }

    [Route("/sensors/{Id}/gaps", "GET")]
    [Route("/sensors/{Id}/gaps", "POST")]
    public partial class SensorGaps
        : IReturn<SensorGapsResponse>
    {
        public SensorGaps()
        {
            GapPoints = new List<GapPoint>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="GapPoints", Description="Collection of GapPoints to insert", ParameterType="body", DataType="List<GapPoint>", IsRequired=true, Verb="POST")]
        public virtual List<GapPoint> GapPoints { get; set; }
    }

    public partial class SensorGapsResponse
        : ResponseBase
    {
        public SensorGapsResponse()
        {
            SensorGaps = new List<GapPoint>{};
        }

        public virtual List<GapPoint> SensorGaps { get; set; }
    }

    [Route("/sensors/{Id}/installations/", "GET")]
    public partial class SensorInstallations
        : IReturn<SensorInstallationsResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    public partial class SensorInstallationsResponse
        : ResponseBase
    {
        public SensorInstallationsResponse()
        {
            Installations = new List<Installation>{};
        }

        public virtual string Id { get; set; }
        public virtual List<Installation> Installations { get; set; }
    }

    [Route("/sensors/{Id}/lastdateprocessed", "GET")]
    [Route("/sensors/{Id}/lastdateprocessed", "POST")]
    public partial class SensorLastDateProcessed
        : IReturn<SensorLastDateProcessedResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Type { get; set; }
        public virtual DateTime? LastDateProcessed { get; set; }
        public virtual string ProcessedStatus { get; set; }
    }

    public partial class SensorLastDateProcessedResponse
        : ResponseBase
    {
        public virtual DateTime? LastDateProcessed { get; set; }
        public virtual string ProcessedStatus { get; set; }
    }

    [Route("/sensors/{Id}/laststatus", "GET")]
    public partial class SensorLastStatus
        : IReturn<SensorLastStatusResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorLastStatusResponse
        : ResponseBase
    {
        public virtual DateTimeOffset LastUpdateDate { get; set; }
        public virtual string LastStatus { get; set; }
    }

    [Route("/sensors/{Id}/lastvalue", "GET")]
    public partial class SensorLastValue
        : IReturn<SensorLastValueResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorLastValueResponse
        : ResponseBase
    {
        public virtual DateTimeOffset LastUpdateDate { get; set; }
        public virtual double LastValue { get; set; }
    }

    [Route("/sensors/{Id}/lock", "PATCH")]
    public partial class SensorLock
        : IReturn<SensorLockResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual bool IsLocked { get; set; }
        public virtual Guid InstallationId { get; set; }
    }

    public partial class SensorLockResponse
        : ResponseBase
    {
    }

    [Route("/thermostats/{Id}/mode", "PATCH")]
    public partial class SensorMode
        : IReturn<SensorModeResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="ModePoint", Description="Sensor mode", ParameterType="body", DataType="ModePoint", IsRequired=true, Verb="PATCH")]
        public virtual ModePoint ModePoint { get; set; }
    }

    public partial class SensorModeResponse
        : ResponseBase
    {
    }

    [Route("/thermostats/modes", "PATCH")]
    public partial class SensorModesBatch
        : IReturn<SensorModesBatchResponse>
    {
        public SensorModesBatch()
        {
            SensorModePoints = new List<SensorModePoint>{};
        }

        [ApiMember(Name="SensorModePoints", Description="Collection of ModePoint to insert", ParameterType="body", DataType="List<SensorModePoint>", IsRequired=true, Verb="PATCH")]
        public virtual List<SensorModePoint> SensorModePoints { get; set; }
    }

    public partial class SensorModesBatchResponse
        : ResponseBase
    {
    }

    [Route("/sensors/{Id}/node", "GET")]
    public partial class SensorNodeRequest
        : IReturn<SensorNodeResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorNodeResponse
        : ResponseBase
    {
        public virtual Node Node { get; set; }
    }

    [Route("/sensors/{Id}/outliers", "GET")]
    [Route("/sensors/{Id}/outliers", "POST")]
    public partial class SensorOutliers
        : IReturn<SensorOutliersResponse>
    {
        public SensorOutliers()
        {
            OutlierPoints = new List<OutlierPoint>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="OutlierPoints", Description="Collection of OutlierPoints to insert", ParameterType="body", DataType="List<OutlierPoint>", IsRequired=true, Verb="POST")]
        public virtual List<OutlierPoint> OutlierPoints { get; set; }
    }

    public partial class SensorOutliersResponse
        : ResponseBase
    {
        public SensorOutliersResponse()
        {
            SensorOutliers = new List<OutlierPoint>{};
        }

        public virtual List<OutlierPoint> SensorOutliers { get; set; }
    }

    [Route("/sensors/{Id}/poll", "GET")]
    public partial class SensorPoll
        : IReturn<SensorPollResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="Guid", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="Installation Id", ParameterType="query", DataType="string", IsRequired=true)]
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorPollResponse
        : ResponseBase
    {
        public virtual SensorPollStatus SensorPollStatus { get; set; }
    }

    [Route("/sensors/{Id}/toggle", "POST")]
    public partial class SensorPowerToggle
        : IReturn<SensorPowerToggleResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string Value { get; set; }
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorPowerToggleResponse
        : ResponseBase
    {
        public virtual bool CommunicationSuccess { get; set; }
        public virtual bool ToggleSuccess { get; set; }
    }

    [Route("/sensors/{Id}/force", "DELETE")]
    public partial class SensorRemoveForce
        : IReturn<SensorRemoveForceResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="GatewayId", Description="Gateway id", ParameterType="body", DataType="Guid", IsRequired=true)]
        public virtual Guid GatewayId { get; set; }
    }

    public partial class SensorRemoveForceResponse
        : ResponseBase
    {
        public virtual bool RemoveSuccess { get; set; }
    }

    [Route("/sensors", "GET")]
    [Route("/sensors", "POST")]
    [Route("/sensors", "PUT")]
    public partial class Sensors
        : IReturn<SensorsResponse>
    {
        [ApiMember(Name="Sensor", Description="Sensor", ParameterType="body", DataType="Sensor", IsRequired=true, Verb="POST")]
        [ApiMember(Name="Sensor", Description="Sensor", ParameterType="body", DataType="Sensor", IsRequired=true, Verb="PUT")]
        public virtual Sensor Sensor { get; set; }

        [ApiMember(Name="OnlyWithoutGateways", Description="Only return sensors with no gateways associated", ParameterType="query", DataType="bool", Verb="GET", ExcludeInSchema=true)]
        public virtual bool OnlyWithoutGateways { get; set; }
    }

    [Route("/thermostats/{Id}/setpoint", "PATCH")]
    public partial class SensorSetPoint
        : IReturn<SensorSetPointResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="SetPointPoint", Description="Sensor setpoint", ParameterType="body", DataType="SetPointPoint", IsRequired=true, Verb="PATCH")]
        public virtual SetPointPoint SetPointPoint { get; set; }
    }

    public partial class SensorSetPointResponse
        : ResponseBase
    {
    }

    [Route("/thermostats/setpoints", "PATCH")]
    public partial class SensorSetPointsBatch
        : IReturn<SensorSetPointsBatchResponse>
    {
        public SensorSetPointsBatch()
        {
            SensorSetPointPoints = new List<SensorSetPointPoint>{};
        }

        [ApiMember(Name="SensorSetPointPoints", Description="Collection of SetPointsPoint to insert", ParameterType="body", DataType="List<SensorSetPointPoint>", IsRequired=true, Verb="PATCH")]
        public virtual List<SensorSetPointPoint> SensorSetPointPoints { get; set; }
    }

    public partial class SensorSetPointsBatchResponse
        : ResponseBase
    {
    }

    [Route("/sensors", "PATCH")]
    public partial class SensorsPatch
        : QueryBase<Sensor>, IReturn<QueryResponse<Sensor>>
    {
        public SensorsPatch()
        {
            Fields = new string[]{};
        }

        [ApiMember(Name="Sensor", Description="Sensor object", ParameterType="body", DataType="Sensor", IsRequired=true, Verb="PATCH")]
        public virtual Sensor Sensor { get; set; }

        [ApiMember(Name="fields", Description="Fields to update", ParameterType="query", DataType="string[]", IsRequired=true, Verb="PATCH")]
        public virtual string[] Fields { get; set; }
    }

    public partial class SensorsResponse
        : ResponseBase
    {
        public SensorsResponse()
        {
            Sensors = new List<Sensor>{};
        }

        public virtual List<Sensor> Sensors { get; set; }
    }

    [Route("/sensors/{Id}/stats", "GET")]
    [Route("/sensors/{Id}/stats", "POST")]
    public partial class SensorsStats
        : IReturn<SensorsStatsResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string FromDate { get; set; }
        public virtual string ToDate { get; set; }
        public virtual int PageNumber { get; set; }
        public virtual int PageSize { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Uptime { get; set; }
    }

    [Route("/sensors/{Id}/stats/month", "GET")]
    public partial class SensorsStatsMonth
        : IReturn<SensorsStatsMonthResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }
    }

    public partial class SensorsStatsMonthResponse
        : ResponseBase
    {
        public virtual double? SensorStatsMonth { get; set; }
    }

    public partial class SensorsStatsResponse
        : ResponseBase
    {
        public SensorsStatsResponse()
        {
            SensorStats = new List<SensorStats>{};
        }

        public virtual List<SensorStats> SensorStats { get; set; }
    }

    [Route("/sensors/{Id}/statuses", "GET")]
    [Route("/sensors/{Id}/statuses", "POST")]
    [Route("/sensors/{Id}/statuses/{TimeStamp}", "PUT")]
    [Route("/sensors/{Id}/statuses/{TimeStamp}", "DELETE")]
    public partial class SensorStatuses
        : IReturn<SensorStatusesResponse>
    {
        public SensorStatuses()
        {
            StatusPoints = new List<StatusPoint>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="StatusPoints", Description="Collection of statuses to insert", ParameterType="body", DataType="List<StatusPoint>", IsRequired=true, Verb="POST")]
        public virtual List<StatusPoint> StatusPoints { get; set; }

        [ApiMember(Name="TimeStamp", Description="Sensor status timestamp", ParameterType="query", DataType="Date", IsRequired=true, Verb="PUT")]
        [ApiMember(Name="TimeStamp", Description="Sensor status timestamp", ParameterType="query", DataType="Date", IsRequired=true, Verb="DELETE")]
        public virtual DateTime TimeStamp { get; set; }

        [ApiMember(Name="Status", Description="Sensor status", ParameterType="body", DataType="string", IsRequired=true, Verb="PUT")]
        public virtual string Status { get; set; }
    }

    [Route("/sensors/statuses", "POST")]
    public partial class SensorStatusesBatch
        : IReturn<SensorStatusesBatchResponse>
    {
        public SensorStatusesBatch()
        {
            SensorStatusPoints = new List<SensorStatusPoint>{};
        }

        [ApiMember(Name="SensorStatusPoints", Description="Collection of statuspoints to insert", ParameterType="body", DataType="List<SensorStatusPoint>", IsRequired=true, Verb="POST")]
        public virtual List<SensorStatusPoint> SensorStatusPoints { get; set; }
    }

    public partial class SensorStatusesBatchResponse
        : ResponseBase
    {
        public SensorStatusesBatchResponse()
        {
            SensorsStatuses = new List<SensorStatusPoint>{};
        }

        public virtual List<SensorStatusPoint> SensorsStatuses { get; set; }
    }

    [Route("/sensors/statuses", "GET")]
    public partial class SensorStatusesHistory
        : IReturn<SensorStatusesHistoryResponse>
    {
        public SensorStatusesHistory()
        {
            Sensors = new List<string>{};
        }

        [ApiMember(Name="Sensors", Description="Collection of sensor ids to get", ParameterType="body", DataType="List<string>", IsRequired=true, Verb="GET")]
        public virtual List<string> Sensors { get; set; }
    }

    public partial class SensorStatusesHistoryResponse
        : ResponseBase
    {
        public SensorStatusesHistoryResponse()
        {
            SensorsStatuses = new List<SensorStatusPoint>{};
        }

        public virtual List<SensorStatusPoint> SensorsStatuses { get; set; }
    }

    public partial class SensorStatusesResponse
        : ResponseBase
    {
        public SensorStatusesResponse()
        {
            Statuses = new List<StatusPoint>{};
        }

        public virtual List<StatusPoint> Statuses { get; set; }
    }

    [Route("/sensors/{SensorId}/thermostat/link", "POST")]
    public partial class SensorThermostatLink
        : IReturn<SensorThermostatLinkResponse>
    {
        [ApiMember(Name="SensorId", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string SensorId { get; set; }

        public virtual string InstallationId { get; set; }
        public virtual bool Link { get; set; }
    }

    public partial class SensorThermostatLinkResponse
        : ResponseBase
    {
        public virtual bool LinkSuccess { get; set; }
    }

    [Route("/sensors/{Id}/thermostat/main", "POST")]
    public partial class SensorThermostatMain
        : IReturn<SensorThermostatMainResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="Installation Id", ParameterType="body", DataType="string", IsRequired=true)]
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorThermostatMainResponse
        : ResponseBase
    {
        public virtual bool IsMain { get; set; }
    }

    [Route("/sensors/{Id}/thermostat/setpoint", "POST")]
    public partial class SensorThermostatSetPoint
        : IReturn<SensorThermostatSetPointResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual double SetPoint { get; set; }
        public virtual bool? IsCelsius { get; set; }
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorThermostatSetPointResponse
        : ResponseBase
    {
        public virtual bool SetPointSuccess { get; set; }
    }

    [Route("/sensors/{Id}/thermostat/setpoint/previous", "POST")]
    public partial class SensorThermostatSetPreviousTemperature
        : IReturn<SensorThermostatSetPointResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="Installation Id", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string InstallationId { get; set; }
    }

    [Route("/sensors/{Id}/thermostat/toggle", "POST")]
    public partial class SensorThermostatToggle
        : IReturn<SensorThermostatToggleResponse>
    {
        [ApiMember(Name="Id", Description="SensorId", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        public virtual string InstallationId { get; set; }
        public virtual bool Value { get; set; }
    }

    public partial class SensorThermostatToggleResponse
        : ResponseBase
    {
        public virtual bool ToggleSuccess { get; set; }
    }

    [Route("/sensors/{Id}/unassign/{InstallationId}", "DELETE")]
    public partial class SensorUnassign
        : IReturn<SensorUnassignResponse>
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="InstallationId", Description="Installation id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string InstallationId { get; set; }
    }

    public partial class SensorUnassignResponse
        : ResponseBase
    {
    }

    [Route("/sensors/{Id}/valuesrange", "DELETE")]
    public partial class SensorValueRange
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="DELETE")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="DELETE")]
        public virtual string ToDate { get; set; }
    }

    [Route("/sensors/{Id}/values", "GET")]
    [Route("/sensors/{Id}/values", "POST")]
    [Route("/sensors/{Id}/values", "PUT")]
    [Route("/sensors/{Id}/values/{TimeStamp}", "DELETE")]
    public partial class SensorValues
        : IReturn<SensorValuesResponse>
    {
        public SensorValues()
        {
            DataPoints = new List<DataPoint>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="Interval", Description="Interval aggregation time in minutes (15 every 15 mins, 30 every half an hour, 60 every hour...)", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int Interval { get; set; }

        [ApiMember(Name="AggregationType", Description="Type of aggregation by interval ('avg' or 'sum')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string AggregationType { get; set; }

        [ApiMember(Name="DataPoints", Description="Collection of datapoints to insert", ParameterType="body", DataType="List<DataPoint>", IsRequired=true, Verb="POST")]
        [ApiMember(Name="DataPoints", Description="Collection of datapoints to insert", ParameterType="body", DataType="List<DataPoint>", IsRequired=true, Verb="PUT")]
        public virtual List<DataPoint> DataPoints { get; set; }

        [ApiMember(Name="TimeStamp", Description="Sensor value timestamp", ParameterType="query", DataType="Date", IsRequired=true, Verb="DELETE")]
        public virtual DateTime TimeStamp { get; set; }

        [ApiMember(Name="Value", Description="Sensor value", ParameterType="body", DataType="double", IsRequired=true, Verb="DELETE")]
        public virtual double Value { get; set; }
    }

    [Route("/sensors/values", "POST")]
    public partial class SensorValuesBatch
        : IReturn<SensorValuesBatchResponse>
    {
        public SensorValuesBatch()
        {
            SensorDataPoints = new List<SensorDataPoint>{};
        }

        [ApiMember(Name="SensorDataPoints", Description="Collection of datapoints to insert", ParameterType="body", DataType="List<SensorDataPoint>", IsRequired=true, Verb="POST")]
        public virtual List<SensorDataPoint> SensorDataPoints { get; set; }
    }

    public partial class SensorValuesBatchResponse
        : ResponseBase
    {
    }

    [Route("/sensors/values/energylive", "POST")]
    public partial class SensorValuesEnergyLiveBatch
        : IReturn<SensorValuesBatchResponse>
    {
        public SensorValuesEnergyLiveBatch()
        {
            SensorDataPoints = new List<SensorDataPoint>{};
        }

        [ApiMember(Name="SensorDataPoints", Description="Collection of datapoints to reflect in live consumption", ParameterType="body", DataType="List<SensorDataPoint>", IsRequired=true, Verb="POST")]
        public virtual List<SensorDataPoint> SensorDataPoints { get; set; }
    }

    [Route("/sensors/{id}/values/excel/{name}", "GET")]
    public partial class SensorValuesExcel
    {
        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Name", Description="Sensor name", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual string Name { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="Interval", Description="Interval aggregation time in minutes (15 every 15 mins, 30 every half an hour, 60 every hour...)", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int Interval { get; set; }

        [ApiMember(Name="AggregationType", Description="Type of aggregation by interval ('avg' or 'sum')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string AggregationType { get; set; }
    }

    [Route("/sensors/{Id}/predictions/values", "GET")]
    [Route("/sensors/{Id}/predictions/values", "POST")]
    public partial class SensorValuesPredictions
        : IReturn<SensorValuesPredictionsResponse>
    {
        public SensorValuesPredictions()
        {
            PredictionPoints = new List<PredictionPoint>{};
        }

        [ApiMember(Name="Id", Description="Sensor id", ParameterType="path", DataType="Guid", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="FromDate", Description="FromDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string FromDate { get; set; }

        [ApiMember(Name="ToDate", Description="ToDate", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string ToDate { get; set; }

        [ApiMember(Name="PageNumber", Description="Pagination parameter page number", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageNumber { get; set; }

        [ApiMember(Name="PageSize", Description="Pagination parameter page size", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int PageSize { get; set; }

        [ApiMember(Name="Interval", Description="Interval aggregation time in minutes (15 every 15 mins, 30 every half an hour, 60 every hour...)", ParameterType="query", DataType="int", Verb="GET")]
        public virtual int Interval { get; set; }

        [ApiMember(Name="AggregationType", Description="Type of aggregation by interval ('avg' or 'sum')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string AggregationType { get; set; }

        [ApiMember(Name="PredictionPoints", Description="Collection of PredictionPoints to insert", ParameterType="body", DataType="List<PredictionPoint>", IsRequired=true, Verb="POST")]
        public virtual List<PredictionPoint> PredictionPoints { get; set; }
    }

    [Route("/sensors/predictions/values", "POST")]
    public partial class SensorValuesPredictionsBatch
        : IReturn<SensorValuesPredictionsBatchResponse>
    {
        public SensorValuesPredictionsBatch()
        {
            PredictionPoints = new List<PredictionPoint>{};
        }

        [ApiMember(Name="PredictionPoint", Description="Collection of predictionpoints to insert", ParameterType="body", DataType="List<PredictionPoint>", IsRequired=true, Verb="POST")]
        public virtual List<PredictionPoint> PredictionPoints { get; set; }
    }

    public partial class SensorValuesPredictionsBatchResponse
        : ResponseBase
    {
    }

    public partial class SensorValuesPredictionsResponse
        : ResponseBase
    {
        public SensorValuesPredictionsResponse()
        {
            SensorValuePredictions = new List<PredictionPoint>{};
        }

        public virtual List<PredictionPoint> SensorValuePredictions { get; set; }
    }

    public partial class SensorValuesResponse
        : ResponseBase
    {
        public SensorValuesResponse()
        {
            Values = new List<DataPoint>{};
        }

        public virtual List<DataPoint> Values { get; set; }
    }

    [Route("/tips", "GET")]
    public partial class Tips
        : IReturn<TipsResponse>
    {
    }

    [Route("/tips/{Section}", "GET")]
    public partial class TipsBySection
        : IReturn<TipsBySectionResponse>
    {
        [ApiMember(Name="Section", Description="Tips section type", ParameterType="path", DataType="TipSection", IsRequired=true, Verb="GET")]
        public virtual TipSection Section { get; set; }
    }

    public partial class TipsBySectionResponse
        : ResponseBase
    {
        public TipsBySectionResponse()
        {
            Tips = new List<Tip>{};
        }

        public virtual List<Tip> Tips { get; set; }
    }

    public partial class TipsResponse
        : ResponseBase
    {
        public TipsResponse()
        {
            Tips = new List<Tip>{};
        }

        public virtual List<Tip> Tips { get; set; }
    }

    [Route("/cameras/unassigned", "GET")]
    public partial class UnassignedCameras
        : IReturn<UnassignedCamerasResponse>
    {
    }

    public partial class UnassignedCamerasResponse
        : ResponseBase
    {
        public UnassignedCamerasResponse()
        {
            UnassignedCameras = new List<Camera>{};
        }

        public virtual List<Camera> UnassignedCameras { get; set; }
    }

    [Route("/installations/unassigned", "GET")]
    public partial class UnassignedInstallations
        : IReturn<UnassignedInstallationsResponse>
    {
    }

    public partial class UnassignedInstallationsResponse
        : ResponseBase
    {
        public UnassignedInstallationsResponse()
        {
            UnassignedInstallations = new List<Installation>{};
        }

        public virtual List<Installation> UnassignedInstallations { get; set; }
    }

    [Route("/sensors/unassigned", "GET")]
    public partial class UnassignedSensors
        : IReturn<UnassignedSensorsResponse>
    {
    }

    public partial class UnassignedSensorsResponse
        : ResponseBase
    {
        public UnassignedSensorsResponse()
        {
            UnassignedSensors = new List<Sensor>{};
        }

        public virtual List<Sensor> UnassignedSensors { get; set; }
    }

    [Route("/users/{Id}/unassigned", "GET")]
    public partial class UnassignedUsers
        : IReturn<UnassignedUsersResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    public partial class UnassignedUsersResponse
        : ResponseBase
    {
        public UnassignedUsersResponse()
        {
            UnassignedUsers = new List<UserAuth>{};
        }

        public virtual List<UserAuth> UnassignedUsers { get; set; }
    }

    [Route("/users/{Id}/block", "POST")]
    public partial class UserBlock
        : IReturn<UserBlockResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Block", Description="Block", ParameterType="body", DataType="bool", IsRequired=true, Verb="POST")]
        public virtual bool Block { get; set; }
    }

    public partial class UserBlockResponse
        : ResponseBase
    {
        public virtual DateTime? LockedDate { get; set; }
    }

    [Route("/users/{Id}/cameras", "GET")]
    public partial class UserCameras
        : IReturn<UserCamerasResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    [Route("/users/{Id}/cameras/count", "GET")]
    public partial class UserCamerasCount
        : IReturn<UserCamerasCountResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class UserCamerasCountResponse
        : ResponseBase
    {
        public virtual int Count { get; set; }
    }

    public partial class UserCamerasResponse
        : ResponseBase
    {
        public UserCamerasResponse()
        {
            Cameras = new List<Camera>{};
        }

        public virtual List<Camera> Cameras { get; set; }
    }

    [Route("/users/{Id}/customers", "GET")]
    public partial class UserCustomers
        : IReturn<UserCustomersResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    [Route("/users/{Id}/customers/count", "GET")]
    public partial class UserCustomersCount
        : IReturn<UserCustomersCountResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class UserCustomersCountResponse
        : ResponseBase
    {
        public virtual int Count { get; set; }
    }

    public partial class UserCustomersResponse
        : ResponseBase
    {
        public UserCustomersResponse()
        {
            Customers = new List<UserAuth>{};
        }

        public virtual List<UserAuth> Customers { get; set; }
    }

    [Route("/users/{Id}", "GET")]
    public partial class UserDetails
        : IReturn<UserDetailsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    public partial class UserDetailsResponse
        : ResponseBase
    {
        public virtual UserAuth User { get; set; }
    }

    [Route("/users/forgottenpassword", "POST")]
    public partial class UserForgottenPassword
        : IReturn<UserForgottenPasswordResponse>
    {
        [ApiMember(Name="Email", Description="User's email", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        public virtual string Email { get; set; }
    }

    public partial class UserForgottenPasswordResponse
        : ResponseBase
    {
    }

    [Route("/users/{Id}/gatewayaccess/{GwId}", "GET")]
    public partial class UserGatewayAccess
        : IReturn<UserGatewayAccessResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="GwId", Description="Gateway id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string GwId { get; set; }
    }

    public partial class UserGatewayAccessResponse
        : ResponseBase
    {
        public virtual bool HasAccess { get; set; }
    }

    [Route("/users/{Id}/gatewayprivilegedaccess/{GwId}", "GET")]
    public partial class UserGatewayPrivilegedAccess
        : IReturn<UserGatewayAccessResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="GwId", Description="Gateway id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string GwId { get; set; }
    }

    [Route("/users/{Id}/installations", "GET")]
    public partial class UserInstallations
        : IReturn<UserInstallationsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    [Route("/users/{Id}/installations/count", "GET")]
    public partial class UserInstallationsCount
        : IReturn<UserInstallationsCountResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class UserInstallationsCountResponse
        : ResponseBase
    {
        public virtual int Count { get; set; }
    }

    public partial class UserInstallationsResponse
        : ResponseBase
    {
        public UserInstallationsResponse()
        {
            Installations = new List<Installation>{};
        }

        public virtual List<Installation> Installations { get; set; }
    }

    [Route("/users/{Id}/settings/notifications", "GET")]
    [Route("/users/{Id}/settings/notifications", "PUT")]
    public partial class UserNotificationSettingsDetails
        : IReturn<UserNotificationSettingsResponse>
    {
        public UserNotificationSettingsDetails()
        {
            NotificationSettings = new List<UserNotificationSettings>{};
        }

        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="PUT", ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        [ApiMember(Name="NotificationSettings", Description="User Settings", ParameterType="body", DataType="List<UserNotificationSettings>", IsRequired=true, Verb="PUT")]
        public virtual List<UserNotificationSettings> NotificationSettings { get; set; }
    }

    public partial class UserNotificationSettingsResponse
        : ResponseBase
    {
        public UserNotificationSettingsResponse()
        {
            NotificationSettings = new List<UserNotificationSettings>{};
        }

        public virtual List<UserNotificationSettings> NotificationSettings { get; set; }
    }

    [Route("/users/{Id}/numnotifications", "GET")]
    [Route("/users/{Id}/numnotifications", "POST")]
    public partial class UserNumNotifications
        : IReturn<UserNumNotificationsResponse>
    {
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        public virtual string NotificationType { get; set; }
        public virtual DateTime NotificationDate { get; set; }
    }

    public partial class UserNumNotificationsResponse
        : ResponseBase
    {
        public virtual UserWithNumNotifications UserBreakdown { get; set; }
    }

    [Route("/users/{Id}/parentusers", "GET")]
    public partial class UserParentUsers
        : IReturn<UserParentUsersResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true)]
        public virtual int Id { get; set; }
    }

    public partial class UserParentUsersResponse
        : ResponseBase
    {
        public UserParentUsersResponse()
        {
            Users = new List<UserAuth>{};
        }

        public virtual List<UserAuth> Users { get; set; }
    }

    [Route("/users/passwordreset", "PATCH")]
    public partial class UserPasswordReset
        : IReturn<UserPasswordResetResponse>
    {
        public virtual Guid Token { get; set; }
        public virtual string Password { get; set; }
        public virtual string PasswordValidation { get; set; }
    }

    public partial class UserPasswordResetResponse
        : ResponseBase
    {
    }

    [Route("/users/passwordresettoken/validate", "GET")]
    public partial class UserPasswordResetTokenValidation
        : IReturn<UserPasswordResetTokenValidationResponse>
    {
        [ApiMember(Name="Token", Description="Password reset token", ParameterType="query", DataType="Guid", IsRequired=true, Verb="GET")]
        public virtual Guid Token { get; set; }
    }

    public partial class UserPasswordResetTokenValidationResponse
        : ResponseBase
    {
        public virtual bool IsValid { get; set; }
    }

    [Route("/users/roles/{role}", "GET")]
    [Route("/users/{Id}/roles", "POST")]
    [Route("/users/{Id}/roles", "DELETE")]
    public partial class UserRoles
        : IReturn<UserRolesResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Role", Description="User rol", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        [ApiMember(Name="Role", Description="User rol", ParameterType="body", DataType="string", IsRequired=true, Verb="POST")]
        [ApiMember(Name="Role", Description="User rol", ParameterType="body", DataType="string", IsRequired=true, Verb="DELETE")]
        public virtual string Role { get; set; }
    }

    public partial class UserRolesResponse
        : ResponseBase
    {
        public UserRolesResponse()
        {
            Users = new List<UserAuth>{};
        }

        public virtual List<UserAuth> Users { get; set; }
    }

    [Route("/users", "PUT")]
    [Route("/users", "POST")]
    public partial class Users
        : IReturn<UserDetailsResponse>
    {
        public virtual UserAuth User { get; set; }
        public virtual string Password { get; set; }
    }

    [Route("/users/{Id}", "DELETE")]
    public partial class UsersDelete
        : IReturn<UsersDeleteResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="int", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        [ApiMember(Name="NewChildrenRefId", Description="RefId to assign to children of deleted user", ParameterType="body", DataType="int?", Verb="DELETE")]
        public virtual int? NewChildrenRefId { get; set; }
    }

    public partial class UsersDeleteResponse
        : ResponseBase
    {
        public virtual bool Success { get; set; }
    }

    [Route("/users/{Id}/sensors", "GET")]
    public partial class UserSensors
        : IReturn<UserSensorsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    [Route("/users/{Id}/sensors/count", "GET")]
    public partial class UserSensorsCount
        : IReturn<UserSensorsCountResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class UserSensorsCountResponse
        : ResponseBase
    {
        public virtual int Count { get; set; }
    }

    public partial class UserSensorsResponse
        : ResponseBase
    {
        public UserSensorsResponse()
        {
            Sensors = new List<Sensor>{};
        }

        public virtual List<Sensor> Sensors { get; set; }
    }

    [Route("/users/{Id}/settings", "GET")]
    [Route("/users/{Id}/settings", "POST")]
    [Route("/users/{Id}/settings", "PUT")]
    public partial class UserSettingsDetails
        : IReturn<UserSettingsResponse>
    {
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        [ApiMember(Name="Id", Description="User Id", ParameterType="path", DataType="int", IsRequired=true, Verb="PUT", ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        [ApiMember(Name="Settings", Description="User Settings", ParameterType="body", DataType="UserSettings", IsRequired=true, Verb="POST")]
        [ApiMember(Name="Settings", Description="User Settings", ParameterType="body", DataType="UserSettings", IsRequired=true, Verb="PUT")]
        public virtual UserSettings Settings { get; set; }
    }

    public partial class UserSettingsResponse
        : ResponseBase
    {
        public virtual UserSettings Settings { get; set; }
    }

    [Route("/users/{Id}/installations", "PUT")]
    public partial class UsersInstallationsBatch
        : IReturn<UsersInstallationsBatchResponse>
    {
        public UsersInstallationsBatch()
        {
            Installations = new List<AssignableInstallation>{};
        }

        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Installations", Description="Installations", ParameterType="body", DataType="List<AssignableInstallation>", IsRequired=true)]
        public virtual List<AssignableInstallation> Installations { get; set; }
    }

    public partial class UsersInstallationsBatchResponse
        : ResponseBase
    {
        public virtual bool Success { get; set; }
    }

    [Route("/users", "GET")]
    public partial class UsersList
        : IReturn<UsersResponse>
    {
    }

    [Route("/users", "PATCH")]
    public partial class UsersPatch
        : IReturn<UserDetailsResponse>
    {
        public UsersPatch()
        {
            Roles = new List<string>{};
        }

        public virtual int Id { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Culture { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual int RefId { get; set; }
        public virtual int? NewChildrenParentRefId { get; set; }
        public virtual List<string> Roles { get; set; }
    }

    [Route("/users", "PATCH")]
    public partial class UsersPatchGeneric
        : QueryBase<UserAuth>, IReturn<QueryResponse<UserAuth>>
    {
        public UsersPatchGeneric()
        {
            Fields = new string[]{};
        }

        [ApiMember(Name="User", Description="User object", ParameterType="body", DataType="UserAuth", IsRequired=true, Verb="PATCH")]
        public virtual UserAuth User { get; set; }

        [ApiMember(Name="fields", Description="Fields to update", ParameterType="query", DataType="string", IsRequired=true, Verb="PATCH")]
        public virtual string[] Fields { get; set; }
    }

    public partial class UsersResponse
        : ResponseBase
    {
        public UsersResponse()
        {
            Users = new List<UserAuth>{};
        }

        public virtual List<UserAuth> Users { get; set; }
    }

    [Route("/users", "PATCH")]
    public partial class UsersSettingsPatch
        : IReturn<UserDetailsResponse>
    {
        public virtual string Id { get; set; }
        public virtual string OldPassword { get; set; }
        public virtual string NewPassword { get; set; }
        public virtual string Email { get; set; }
        public virtual string Culture { get; set; }
        public virtual string TimeZone { get; set; }
    }

    [Route("/users/{Id}/users", "GET")]
    public partial class UserSubusers
        : IReturn<UserUsersResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }
    }

    [Route("/users/{Id}/users", "GET")]
    [Route("/users/{Id}/users", "POST")]
    public partial class UserUsers
        : IReturn<UserUsersResponse>
    {
        public UserUsers()
        {
            ListUsersID = new List<int>{};
        }

        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET", ExcludeInSchema=true)]
        public virtual string Id { get; set; }

        [ApiMember(Name="Id", Description="User id", ParameterType="body", DataType="List<int>", IsRequired=true, Verb="POST")]
        public virtual List<int> ListUsersID { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    [Route("/users/{Id}/users/count", "GET")]
    public partial class UserUsersCount
        : IReturn<UserUsersCountResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="string", IsRequired=true, Verb="GET")]
        public virtual string Id { get; set; }

        [ApiMember(Name="Type", Description="Type (empty or 'Error')", ParameterType="query", DataType="string", Verb="GET")]
        public virtual string Type { get; set; }
    }

    public partial class UserUsersCountResponse
        : ResponseBase
    {
        public virtual int Count { get; set; }
    }

    public partial class UserUsersResponse
        : ResponseBase
    {
        public UserUsersResponse()
        {
            Users = new List<UserAuth>{};
        }

        public virtual List<UserAuth> Users { get; set; }
    }

    [Route("/userwarnings/details", "GET")]
    [Route("/userwarnings", "POST")]
    [Route("/userwarnings/{Id}", "DELETE")]
    public partial class UserWarnings
        : IReturn<UserWarningsResponse>
    {
        [ApiMember(Name="UserWarning", Description="Sensor Warning", ParameterType="body", DataType="UserWarning", IsRequired=true, Verb="POST")]
        public virtual UserWarning UserWarning { get; set; }

        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="string", IsRequired=true, Verb="DELETE", ExcludeInSchema=true)]
        public virtual int Id { get; set; }
    }

    [Route("/users/{Id}/warnings/active", "GET")]
    public partial class UserWarningsActive
        : IReturn<WarningsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="int", IsRequired=true)]
        public virtual int Id { get; set; }
    }

    [Route("/users/{Id}/warnings/created", "GET")]
    public partial class UserWarningsCreated
        : IReturn<WarningsResponse>
    {
        [ApiMember(Name="Id", Description="User id", ParameterType="path", DataType="int", IsRequired=true)]
        public virtual int Id { get; set; }
    }

    public partial class UserWarningsResponse
        : ResponseBase
    {
        public UserWarningsResponse()
        {
            UserWarnings = new List<UserWarning>{};
        }

        public virtual List<UserWarning> UserWarnings { get; set; }
    }

    [Route("/warnings/{Id}/activate", "POST")]
    public partial class WarningActivate
        : IReturn<WarningActivateResponse>
    {
        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="int", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        [ApiMember(Name="Activate", Description="Activate indicator", ParameterType="body", DataType="bool", IsRequired=true, Verb="POST")]
        public virtual bool Activate { get; set; }
    }

    public partial class WarningActivateResponse
        : ResponseBase
    {
    }

    [Route("/warnings/{id}/users/assignable", "GET")]
    public partial class WarningAssignableUsers
        : IReturn<WarningAssignableUsersResponse>
    {
        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="int", IsRequired=true)]
        public virtual int Id { get; set; }
    }

    public partial class WarningAssignableUsersResponse
        : ResponseBase
    {
        public WarningAssignableUsersResponse()
        {
            Users = new List<AssignableUser>{};
        }

        public virtual List<AssignableUser> Users { get; set; }
    }

    [Route("/warnings/{Id}/details", "GET")]
    [Route("/warnings/{Id}", "DELETE")]
    public partial class WarningDetails
        : IReturn<WarningDetailsResponse>
    {
        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="int", IsRequired=true, ExcludeInSchema=true)]
        public virtual int Id { get; set; }
    }

    public partial class WarningDetailsResponse
        : ResponseBase
    {
        public virtual Warning Warning { get; set; }
    }

    [Route("/warnings/{Id}/immediate", "POST")]
    public partial class WarningImmediate
        : IReturn<WarningActivateResponse>
    {
        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="int", IsRequired=true, Verb="POST", ExcludeInSchema=true)]
        public virtual int Id { get; set; }
    }

    [Route("/warnings", "GET")]
    [Route("/warnings", "POST")]
    [Route("/warnings", "PUT")]
    public partial class Warnings
        : IReturn<WarningsResponse>
    {
        [ApiMember(Name="warning", Description="Warning definition", ParameterType="body", DataType="Warning", IsRequired=true, Verb="POST")]
        [ApiMember(Name="warning", Description="Warning definition", ParameterType="body", DataType="Warning", IsRequired=true, Verb="PUT")]
        public virtual Warning Warning { get; set; }
    }

    [Route("/warnings", "PATCH")]
    public partial class WarningsPatch
        : QueryBase<Warning>, IReturn<QueryResponse<Warning>>
    {
        public WarningsPatch()
        {
            Fields = new string[]{};
        }

        [ApiMember(Name="warning", Description="Warning definition", ParameterType="body", DataType="Warning", IsRequired=true, Verb="PATCH")]
        public virtual Warning Warning { get; set; }

        [ApiMember(Name="fields", Description="Fields to update", ParameterType="query", DataType="string", IsRequired=true, Verb="PATCH")]
        public virtual string[] Fields { get; set; }
    }

    public partial class WarningsResponse
        : ResponseBase
    {
        public WarningsResponse()
        {
            Warnings = new List<Warning>{};
        }

        public virtual List<Warning> Warnings { get; set; }
    }

    [Route("/warnings/{Id}/users", "GET")]
    [Route("/warnings/{Id}/users", "PUT")]
    public partial class WarningUsers
        : IReturn<WarningUsersResponse>
    {
        public WarningUsers()
        {
            Users = new List<AssignableUser>{};
        }

        [ApiMember(Name="Id", Description="Warning id", ParameterType="path", DataType="int", IsRequired=true, ExcludeInSchema=true)]
        public virtual int Id { get; set; }

        [ApiMember(Name="Users", Description="List of assignable users", ParameterType="body", DataType="List<AssignableUser>", IsRequired=true, Verb="PUT")]
        public virtual List<AssignableUser> Users { get; set; }
    }

    public partial class WarningUsersResponse
        : ResponseBase
    {
        public WarningUsersResponse()
        {
            WarningUsers = new List<UserAuth>{};
        }

        public virtual List<UserAuth> WarningUsers { get; set; }
    }
}

namespace nassist.ServiceModel
{

    public partial class Area
    {
        public virtual int Id { get; set; }
        public virtual int FloorplanId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
    }

    public partial class AssignableArea
        : Area
    {
        public virtual bool IsAssigned { get; set; }
    }

    public partial class AssignableInstallation
        : Installation
    {
        public virtual bool IsAssigned { get; set; }
    }

    public partial class AssignableUser
        : UserAuth
    {
        public virtual bool IsAssigned { get; set; }
    }

    public partial class BatteryLevelReport
    {
        public virtual string SensorId { get; set; }
        public virtual double BatteryLevel { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual bool Notify { get; set; }
    }

    public partial class ComfortArea
    {
        public ComfortArea()
        {
            Thermostat = new List<Sensor>{};
            ACIRTransmitter = new List<Sensor>{};
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual Sensor MainThermostat { get; set; }
        public virtual List<Sensor> Thermostat { get; set; }
        public virtual List<Sensor> ACIRTransmitter { get; set; }
        public virtual double? AverageTemperature { get; set; }
        public virtual double? AverageHumidity { get; set; }
        public virtual double? AverageCO2 { get; set; }
        public virtual double? AverageBrightness { get; set; }
        public virtual double? AverageNO { get; set; }
        public virtual double? AverageNO2 { get; set; }
        public virtual double? AverageO3 { get; set; }
        public virtual double? AverageAtmosphericPressure { get; set; }
    }

    public partial class ComfortMonthValues
    {
        public virtual int Id { get; set; }
        public virtual Guid InstallationId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Month { get; set; }
        public virtual string Year { get; set; }
        public virtual bool W1Tick { get; set; }
        public virtual bool W2Tick { get; set; }
        public virtual bool W3Tick { get; set; }
        public virtual bool W4Tick { get; set; }
        public virtual int W1Temp { get; set; }
        public virtual int W2Temp { get; set; }
        public virtual int W3Temp { get; set; }
        public virtual int W4Temp { get; set; }
    }

    public partial class ComfortValues
    {
        public virtual double? TemperatureInside { get; set; }
        public virtual double? Humidity { get; set; }
        public virtual Sensor MainThermostat { get; set; }
        public virtual Sensor TemperatureOutside { get; set; }
        public virtual Sensor Forecast { get; set; }
    }

    public partial class DefaultSensorConfiguration
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string GatewayType { get; set; }
        public virtual string Protocol { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string ManufacturerType { get; set; }
        public virtual string ManufacturerTypeId { get; set; }
        public virtual string ManufacturerTypeName { get; set; }
        public virtual string FirmwareVersion { get; set; }
        public virtual string ManufacturerName { get; set; }
        public virtual string ManufacturerDeviceName { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
        public virtual string MinGWVersion { get; set; }
        public virtual string SensorConfiguration { get; set; }
        [Ignore]
        public virtual SensorConfiguration Configuration { get; set; }
    }

    public partial class Device
    {
        public Device()
        {
            Children = new List<Sensor>{};
        }

        public virtual Sensor Parent { get; set; }
        public virtual List<Sensor> Children { get; set; }
    }

    public partial class DeviceArea
    {
        public DeviceArea()
        {
            Children = new List<SensorArea>{};
        }

        public virtual SensorArea Parent { get; set; }
        public virtual List<SensorArea> Children { get; set; }
    }

    public partial class Floorplan
    {
        public virtual int Id { get; set; }
        public virtual Guid InstallationId { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageUri { get; set; }
        public virtual float? X_Ini { get; set; }
        public virtual float? X_End { get; set; }
        public virtual float? Y_Ini { get; set; }
        public virtual float? Y_End { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
    }

    public partial class FloorPlanWithAreas
        : Floorplan
    {
        public FloorPlanWithAreas()
        {
            Areas = new List<AssignableArea>{};
        }

        public virtual List<AssignableArea> Areas { get; set; }
    }

    public partial class GapPoint
    {
        public virtual string Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Value { get; set; }
    }

    public partial class Installation
    {
        public virtual Guid Id { get; set; }
        public virtual string Type { get; set; }
        public virtual int? OwnerId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Status { get; set; }
        public virtual string SecurityStatus { get; set; }
        public virtual DateTime? LastDateStatus { get; set; }
        public virtual double? Latitude { get; set; }
        public virtual double? Longitude { get; set; }
        public virtual int? City { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
        public virtual DateTime? LastDateAverageCalculations { get; set; }
        public virtual DateTime? LastAliveDate { get; set; }
        public virtual double? AverageTemperature { get; set; }
        public virtual double? AverageHumidity { get; set; }
        public virtual double? AverageAirQuality { get; set; }
        public virtual string DayEnergyTrendCode { get; set; }
        public virtual string WeekEnergyTrendCode { get; set; }
        public virtual string MonthEnergyTrendCode { get; set; }
        public virtual string DayGasTrendCode { get; set; }
        public virtual string WeekGasTrendCode { get; set; }
        public virtual string MonthGasTrendCode { get; set; }
        public virtual string DayHeatingTrendCode { get; set; }
        public virtual string WeekHeatingTrendCode { get; set; }
        public virtual string MonthHeatingTrendCode { get; set; }
        public virtual string DayWaterTrendCode { get; set; }
        public virtual string WeekWaterTrendCode { get; set; }
        public virtual string MonthWaterTrendCode { get; set; }
        public virtual double? DayEnergyTrendValue { get; set; }
        public virtual double? WeekEnergyTrendValue { get; set; }
        public virtual double? MonthEnergyTrendValue { get; set; }
        public virtual double? DayGasTrendValue { get; set; }
        public virtual double? WeekGasTrendValue { get; set; }
        public virtual double? MonthGasTrendValue { get; set; }
        public virtual double? DayHeatingTrendValue { get; set; }
        public virtual double? WeekHeatingTrendValue { get; set; }
        public virtual double? MonthHeatingTrendValue { get; set; }
        public virtual double? DayWaterTrendValue { get; set; }
        public virtual double? WeekWaterTrendValue { get; set; }
        public virtual double? MonthWaterTrendValue { get; set; }
        public virtual DateTime? LastDateEnergyTrend { get; set; }
        public virtual DateTime? LastDateGasTrend { get; set; }
        public virtual DateTime? LastDateHeatingTrend { get; set; }
        public virtual DateTime? LastDateWaterTrend { get; set; }
        public virtual string ComfortStatus { get; set; }
        public virtual double Uptime { get; set; }
        public virtual int ActiveSchedulesComfort { get; set; }
        public virtual int ActiveSchedulesSecurity { get; set; }
        public virtual int ActiveSchedulesControl { get; set; }
        public virtual double? PricekWh { get; set; }
        public virtual double? PriceGaskWh { get; set; }
        public virtual double? PriceHeatingkWh { get; set; }
        public virtual double? PriceWaterM3 { get; set; }
        public virtual double? GasMeterTokWh { get; set; }
        public virtual string DashboardView { get; set; }
        public virtual DateTime? ActivationDate { get; set; }
        public virtual bool Blocked { get; set; }
        public virtual DateTime? LastLogUploadDate { get; set; }
        public virtual string LastLogName { get; set; }
        public virtual string GWVersion { get; set; }
    }

    public partial class InstallationCategoryConsumption
    {
        public virtual int Id { get; set; }
        public virtual Guid InstallationId { get; set; }
        public virtual string Category { get; set; }
        public virtual double Day { get; set; }
        public virtual double Week { get; set; }
        public virtual double Month { get; set; }
    }

    public partial class InstallationCategoryPeriodConsumption
    {
        public virtual string ConsumptionCategory { get; set; }
        public virtual string ConsumptionPeriod { get; set; }
        public virtual double Value { get; set; }
        public virtual string DayName { get; set; }
        public virtual int Year { get; set; }
        public virtual int? Week { get; set; }
        public virtual int Month { get; set; }
        public virtual int? Day { get; set; }
        public virtual int? Hour { get; set; }
    }

    public partial class InstallationStats
    {
        public virtual string Id { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual double? Uptime { get; set; }
    }

    public partial class InstallationWithSensorAndCategory
    {
        public virtual Guid InstallationId { get; set; }
        public virtual string SensorId { get; set; }
        public virtual string ConsumptionCategory { get; set; }
    }

    public partial class IPScanInfo
    {
        public virtual string IP { get; set; }
        public virtual string HostName { get; set; }
        public virtual string Port { get; set; }
    }

    public partial class Node
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Description { get; set; }
        public virtual string Remarks { get; set; }
        public virtual float? PositionX { get; set; }
        public virtual float? PositionY { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
    }

    public partial class OutlierPoint
    {
        public virtual string Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Value { get; set; }
        public virtual double CorrectedValue { get; set; }
        public virtual bool Processed { get; set; }
    }

    public partial class Photo
    {
        public virtual string Url { get; set; }
        public virtual string TriggerId { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public partial class PredictionPoint
    {
        public virtual string Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double Value { get; set; }
        public virtual bool Peak { get; set; }
    }

    public partial class SchedulerInstance
    {
        public SchedulerInstance()
        {
            SchedulerPoints = new List<SchedulerPoint>{};
        }

        public virtual SchedulerType Name { get; set; }
        public virtual bool Status { get; set; }
        public virtual List<SchedulerPoint> SchedulerPoints { get; set; }
    }

    public partial class SchedulerPoint
    {
        public virtual string Day { get; set; }
        public virtual int Hour { get; set; }
        public virtual int Minutes { get; set; }
        public virtual string Security { get; set; }
        public virtual int Temperature { get; set; }
        public virtual string Automation { get; set; }
        public virtual string SensorId { get; set; }
    }

    public enum SchedulerType
    {
        Comfort,
        Security,
        Control,
    }

    public partial class Sensor
        : SensorBase
    {
        public virtual double? Value { get; set; }
        public virtual string Status { get; set; }
        public virtual string PreviousStatus { get; set; }
        public virtual string Mode { get; set; }
        public virtual string PreviousMode { get; set; }
        public virtual double? SetPoint { get; set; }
        public virtual double? PreviousSetPoint { get; set; }
        public virtual double? BatteryLevel { get; set; }
        public virtual DateTime? LastDateValue { get; set; }
        public virtual DateTime? LastDateStatus { get; set; }
        public virtual DateTime? LastDateMode { get; set; }
        public virtual DateTime? LastDateSetPoint { get; set; }
        public virtual DateTime? LastDateBattery { get; set; }
        public virtual DateTime LastModificationDate { get; set; }
        public virtual bool? IsVirtual { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual string Configuration { get; set; }
        [Ignore]
        public virtual SensorConfiguration Config { get; set; }
    }

    public partial class SensorArea
    {
        public virtual Sensor Sensor { get; set; }
        public virtual string Area { get; set; }
    }

    public partial class SensorBase
    {
        public virtual string Id { get; set; }
        public virtual string ParentId { get; set; }
        public virtual string Protocol { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string ManufacturerType { get; set; }
        public virtual string ManufacturerTypeId { get; set; }
        public virtual string ManufacturerTypeName { get; set; }
        public virtual string FirmwareVersion { get; set; }
        public virtual string ManufacturerName { get; set; }
        public virtual string ManufacturerDeviceName { get; set; }
        public virtual string Description { get; set; }
        public virtual string Remarks { get; set; }
        public virtual string ConsumptionCategory { get; set; }
        public virtual DateTime CreationDate { get; set; }
    }

    public partial class SensorBaseWithAreaIDandName
        : SensorBase
    {
        public virtual int AreaId { get; set; }
        public virtual string AreaName { get; set; }
    }

    public partial class SensorConfiguration
    {
        public SensorConfiguration()
        {
            Properties = new Dictionary<string, Object>{};
            EditableProperties = new Dictionary<string, Object>{};
        }

        public virtual Dictionary<string, Object> Properties { get; set; }
        public virtual Dictionary<string, Object> EditableProperties { get; set; }
    }

    public enum SensorProtocolType
    {
        WMBus,
        ZWave,
    }

    public partial class SensorStats
    {
        public virtual string Id { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual double? Uptime { get; set; }
    }

    public partial class SensorWithAreaIdAndName
        : Sensor
    {
        public virtual int AreaId { get; set; }
        public virtual string AreaName { get; set; }
    }

    public partial class SensorWithAreaName
        : Sensor
    {
        public virtual string AreaName { get; set; }
    }

    public partial class Tip
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Text { get; set; }
        public virtual string Description { get; set; }
        public virtual string Icon { get; set; }
        public virtual string Section { get; set; }
    }

    public enum TipSection
    {
        Electricity,
        Gas,
        Heating,
        Water,
    }

    public partial class UserAuth
    {
        public UserAuth()
        {
            Roles = new List<string>{};
            Permissions = new List<string>{};
            Meta = new Dictionary<string, string>{};
        }

        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string PrimaryEmail { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Country { get; set; }
        public virtual string Culture { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string Language { get; set; }
        public virtual string MailAddress { get; set; }
        public virtual string Nickname { get; set; }
        public virtual string PostalCode { get; set; }
        public virtual string TimeZone { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public virtual List<string> Roles { get; set; }
        public virtual List<string> Permissions { get; set; }
        public virtual int? RefId { get; set; }
        public virtual string RefIdStr { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Company { get; set; }
        public virtual string Address { get; set; }
        public virtual string Address2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual int InvalidLoginAttempts { get; set; }
        public virtual DateTime? LastLoginAttempt { get; set; }
        public virtual DateTime? LockedDate { get; set; }
        public virtual string RecoveryToken { get; set; }
        public virtual Dictionary<string, string> Meta { get; set; }
    }

    public partial class UserNotificationSettings
    {
        public virtual int Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual string SubType { get; set; }
        public virtual bool SMS { get; set; }
        public virtual bool Mail { get; set; }
        public virtual bool Push { get; set; }
        public virtual bool IsEnabled { get; set; }
    }

    public partial class UserSettings
    {
        public virtual int Id { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual bool CelsiusUnits { get; set; }
        public virtual string Currency { get; set; }
        public virtual string DefaultInstallation { get; set; }
        public virtual bool DREnabled { get; set; }
        public virtual string DefaultPage { get; set; }
    }

    public partial class UserWarning
    {
        public virtual long Id { get; set; }
        public virtual int UserId { get; set; }
        public virtual int WarningId { get; set; }
    }

    public partial class UserWithNumNotifications
    {
        public virtual UserAuth User { get; set; }
        public virtual int MailNotifications { get; set; }
        public virtual int PushNotifications { get; set; }
        public virtual int SMSNotifications { get; set; }
    }

    public partial class Warning
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual string Message { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime FromDate { get; set; }
        public virtual DateTime ToDate { get; set; }
        public virtual bool IsActivated { get; set; }
        public virtual bool IsPersistent { get; set; }
        public virtual bool IsImmediate { get; set; }
        public virtual string I18N { get; set; }
    }
}

namespace nassist.ServiceModel.Camera
{

    public partial class Camera
    {
        public virtual string Id { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual int Port { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string Type { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string Model { get; set; }
        public virtual DateTime? LastDateImage { get; set; }
        public virtual string LastImageName { get; set; }
        public virtual DateTime? CreationDate { get; set; }
        public virtual string LastSensorId { get; set; }
        public virtual string Configuration { get; set; }
    }

    public partial class CameraNode
    {
        public virtual int NodeId { get; set; }
        public virtual string CameraId { get; set; }
    }
}

namespace nassist.ServiceModel.Prosyst
{

    public partial class CommandResponseWrapper<Q>
    {
        public virtual Q Response { get; set; }
        public virtual bool CommunicationSuccess { get; set; }
    }

    public partial class ZWaveRegister
    {
        public virtual int ParamId { get; set; }
        public virtual int Length { get; set; }
        public virtual int Value { get; set; }
    }
}

namespace nassist.ServiceModel.UPnP
{

    public partial class UPnPDeviceInfo
    {
        public virtual string IP { get; set; }
        public virtual string Port { get; set; }
        public virtual string UDN { get; set; }
        public virtual string FriendlyName { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual string ManufacturerURL { get; set; }
        public virtual string ModelName { get; set; }
        public virtual string ModelDescription { get; set; }
        public virtual string ModelNumber { get; set; }
        public virtual string SerialNumber { get; set; }
        public virtual string PresentationURL { get; set; }
    }
}

namespace nassist.Shared
{

    public partial class BarChartCategorizedItem
    {
        public BarChartCategorizedItem()
        {
            Values = new Dictionary<string, double>{};
        }

        public virtual string Marker { get; set; }
        public virtual Dictionary<string, double> Values { get; set; }
    }

    public partial class InstallationDataPoint
    {
        public virtual double? AverageTemperature { get; set; }
        public virtual double? AverageHumidity { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public partial class SensorPollStatus
    {
        public virtual string Status { get; set; }
        public virtual int? BatteryLevel { get; set; }
    }
}

namespace nassist.Shared.Constants
{

    public enum MasterConsumptionCategoryType
    {
        master,
        gasmaster,
        heatingmaster,
        watermaster,
    }
}

namespace nassist.Shared.SensorData
{

    public partial class DataPoint
    {
        public virtual double Value { get; set; }
        public virtual DateTime Date { get; set; }
    }

    public partial class ModePoint
    {
        public virtual string Mode { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Trigger { get; set; }
        public virtual string TriggerName { get; set; }
    }

    public partial class SensorDataPoint
    {
        public virtual string SensorId { get; set; }
        public virtual string SensorName { get; set; }
        public virtual DataPoint DataPoint { get; set; }
    }

    public partial class SensorModePoint
    {
        public virtual string SensorId { get; set; }
        public virtual ModePoint ModePoint { get; set; }
    }

    public partial class SensorSetPointPoint
    {
        public virtual string SensorId { get; set; }
        public virtual SetPointPoint SetPointPoint { get; set; }
    }

    public partial class SensorStatusPoint
    {
        public virtual string SensorId { get; set; }
        public virtual StatusPoint StatusPoint { get; set; }
    }

    public partial class SetPointPoint
    {
        public virtual double SetPoint { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Trigger { get; set; }
        public virtual string TriggerName { get; set; }
    }

    public partial class StatusPoint
    {
        public virtual string Status { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Trigger { get; set; }
        public virtual string TriggerName { get; set; }
    }
}

