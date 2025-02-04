$(function () {
    jQuery('[data-confirm]').click(function (e) {
        if (!confirm(jQuery(this).attr("data-confirm"))) {
            e.preventDefault()
        }
    });

    
    const toastLiveExample = document.getElementById("liveToast");
    var message = $("#hdn-transaction-message").val();
    
    if (message != null && message.length > 0) {        
        const toast = new bootstrap.Toast(toastLiveExample);
        toast.show();
    }   

    $("#dialog-confirm").dialog({
        resizable: false,
        height: "auto",
        width: 400,
        modal: true,
        buttons: {
            "Yes": function () {
                $(this).dialog("close");
            },
            Cancel: function () {
                $(this).dialog("close");
            }
        }
    });
});

function populateEditAgent(jsonData) {
   
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditAgent");
    
    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $("#Agent_AgentId").val(data.AgentId);
        $("#Agent_AddressId").val(data.AddressId);
        $("#Agent_Name").val(data.Name);
        $("#Agent_DBA").val(data.DBA);
        $("#Agent_AddressLine1").val(data.AddressLine1);
        $("#Agent_AddressLine2").val(data.AddressLine2);
        $("#Agent_City").val(data.City);
        $("#Agent_State").val(data.State);
        $("#Agent_PostalCode").val(data.PostalCode);
        $("#Agent_CountryId").val(data.CountryId)
    }
}

function populateEditAgentContact(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditAgentContact");

    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $("#AgentContact_AgentContactId").val(data.AgentContactId);
        $("#AgentContact_TitleId").val(data.TitleId);
        $("#AgentContact_FirstName").val(data.FirstName);
        $("#AgentContact_LastName").val(data.LastName);
        $("#AgentContact_Email").val(data.Email);
        $("#AgentContact_Phone").val(data.Phone);           
        $("#AgentContact_IsActive").val(data.IsActive.toString());  
    }
}

function populateCreateAgentContact(jsonData) {
    
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditAgentContact");

    var data = $.parseJSON(jsonData);

    if (data !== null) {     
        $(".title").attr("readonly", "readonly");
        $(".firstname").attr("readonly", "readonly");
        $(".lastname").attr("readonly", "readonly");
        $(".email").attr("readonly", "readonly");
        $(".phone").attr("readonly", "readonly");

        $(".secondaryId").val(data.AgentContactId);
        $(".title select").val(data.Title);
        $(".firstname").val(data.FirstName);
        $(".lastname").val(data.LastName);
        $(".email").val(data.Email);
        $(".phone").val(data.Phone);        
    }
}

function populateEditBroker(jsonData) {

    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditBroker");

    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $("#Broker_BrokerId").val(data.BrokerId);
        $("#Broker_AddressId").val(data.AddressId);
        $("#Broker_Name").val(data.Name);
        $("#Broker_DBA").val(data.DBA);
        $("#Broker_AddressLine1").val(data.AddressLine1);
        $("#Broker_AddressLine2").val(data.AddressLine2);
        $("#Broker_City").val(data.City);
        $("#Broker_State").val(data.State);
        $("#Broker_PostalCode").val(data.PostalCode);
        $("#Broker_CountryId").val(data.CountryId)
    }
}

function populateEditCarrier(jsonData) {

    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditCarrier");

    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $("#Carrier_CarrierId").val(data.CarrierId);
        $("#Carrier_AddressId").val(data.AddressId);
        $("#Carrier_Name").val(data.Name);
        $("#Carrier_DBA").val(data.DBA);
        $("#Carrier_AddressLine1").val(data.AddressLine1);
        $("#Carrier_AddressLine2").val(data.AddressLine2);
        $("#Carrier_City").val(data.City);
        $("#Carrier_State").val(data.State);
        $("#Carrier_PostalCode").val(data.PostalCode);
        $("#Carrier_CountryId").val(data.CountryId)
    }
}

function populateCreateBrokerContact(jsonData) { 

    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateCreateBrokerContact");  

    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $(".title").attr("readonly", "readonly");
        $(".firstname").attr("readonly", "readonly");
        $(".lastname").attr("readonly", "readonly");
        $(".email").attr("readonly", "readonly");
        $(".phone").attr("readonly", "readonly");

        $(".secondaryId").val(data.BrokerContactId);
        $(".title select").val(data.Title);
        $(".firstname").val(data.FirstName);
        $(".lastname").val(data.LastName);
        $(".email").val(data.Email);
        $(".phone").val(data.Phone);
    }
}

function populateEditBrokerContact(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditBrokerContact");

    var data = $.parseJSON(jsonData);

    alert(data.BrokerContactId);

    if (data !== null) {
        $("#BrokerContact_BrokerContactId").val(data.BrokerContactId);
        $("#BrokerContact_TitleId").val(data.TitleId);
        $("#BrokerContact_FirstName").val(data.FirstName);
        $("#BrokerContact_LastName").val(data.LastName);
        $("#BrokerContact_Email").val(data.Email);
        $("#BrokerContact_Phone").val(data.Phone);
        $("#BrokerContact_IsActive").val(data.IsActive.toString());
    }
}

function populateCreateCarrierContact(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateCreateCarrierContact");
   
    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $(".title").attr("readonly", "readonly");
        $(".firstname").attr("readonly", "readonly");
        $(".lastname").attr("readonly", "readonly");
        $(".email").attr("readonly", "readonly");
        $(".phone").attr("readonly", "readonly");

        $(".secondaryId").val(data.CarrierContactId);
        $(".title select").val(data.Title);
        $(".firstname").val(data.FirstName);
        $(".lastname").val(data.LastName);
        $(".email").val(data.Email);
        $(".phone").val(data.Phone);
    }
}
function populateEditCarrierContact(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditCarrierContact");

    var data = $.parseJSON(jsonData);

    if (data !== null) {
        $("#CarrierContact_CarrierContactId").val(data.CarrierContactId);
        $("#CarrierContact_TitleId").val(data.TitleId);
        $("#CarrierContact_FirstName").val(data.FirstName);
        $("#CarrierContact_LastName").val(data.LastName);
        $("#CarrierContact_Email").val(data.Email);
        $("#CarrierContact_Phone").val(data.Phone);
        $("#CarrierContact_IsActive").val(data.IsActive.toString());
    }
}

function populateEditUserAccount(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditUserAccount");    

    var data = $.parseJSON(jsonData);    

    if (data !== null) {
        $("#UserAccount_UserId").val(data.UserId);
        $("#UserAccount_FullName").val(data.FullName);
        $("#UserAccount_Email").val(data.Email);
        $("#UserAccount_Phone").val(data.Phone);
        $("#UserAccount_ContactRole").val(data.ContactRole);
        $("#UserAccount_IsAdmin").val(data.IsAdmin.toString());
        $("#UserAccount_LockUser").val(data.LockUser.toString());
        $("#UserAccount_IsMFAEnabled").val(data.IsMFAEnabled.toString());
        $("#UserAccount_ConfirmedEmail").val(data.ConfirmedEmail.toString());
    }
}

function populateUserPasswordChange(jsonData) {
    if (jsonData.length === 0)
        console.log("error has occurred while reading json data in populateEditUserAccount");

    var data = $.parseJSON(jsonData); 

    if (data !== null) {
        $(".userid").val(data.UserId);
        $(".username").val(data.Email);
        $(".email").val(data.Email);
    }
}