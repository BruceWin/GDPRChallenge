if (typeof (BW) === "undefined") {
    BW = {
        __namespace: true
    };
}
if (typeof (BW.Scripts) === "undefined") {
    BW.Scripts = {
        __namespace: true
    };
}

if (typeof (BW.Scripts.Account) === "undefined") {
    BW.Scripts.Account = {
        __namespace: true
    };
}

BW.Scripts.Account.Form = (function () {
    var onLoad = function onLoad(executionContext) {
        var formContext = executionContext.getFormContext();
        if (formContext.getAttribute('bw_legalhold').getValue()) {
            formContext.getAttribute('bw_legalholdreason').setRequiredLevel('required');
        }
    };
    var addLegalHold = function addLegalHold(executionContext) {
        var formContext = executionContext.getFormContext();
        if (formContext.getAttribute('bw_legalhold').getValue()) {
            Xrm.Navigation.openAlertDialog({ confirmButtonLabel: "OK", text: "A Legal Hold has been applied.\nData retention rules will no longer be applied to this account and related data.", title: "Legal Hold Applied" })
                .then(function (success) {
                    formContext.getAttribute('bw_legalholdreason').setRequiredLevel('required');

                }, function (error) { });
        }
    };

    return {
        addLegalHold: addLegalHold,
        onLoad: onLoad
    };

})();