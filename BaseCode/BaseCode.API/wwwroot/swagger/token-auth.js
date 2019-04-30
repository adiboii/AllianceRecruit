(function () {
    $(function () {
        var basicAuthUI =
            '<div class="input" style = "padding-left: 10px;"><input placeholder="token" id="auth_Key" name="auth_Key" type="text"></div>';
        $(basicAuthUI).insertAfter('#api_selector #input_baseUrl');
        $("#select_document").hide();
        $('#auth_Key').change(addAuthorization);
    });
    function addAuthorization() {
        var auth_Key = $('#auth_Key').val();
        if (auth_Key.trim() !== "") {
            window.swaggerUi.api.clientAuthorizations.add("key", new SwaggerClient.ApiKeyAuthorization("Authorization", 'Bearer' + ' ' + auth_Key, "header"));
        }
    }
})();