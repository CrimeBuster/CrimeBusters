<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPage.aspx.cs" Inherits="CrimeBusters.WebApp.TestPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script>
        $(function () {
            $(document).on("click", "input#createReport", function (e) {
                e.preventDefault();

                var formData = new FormData();
                formData.append("photo", $("input#uploadPicFile").get(0).files[0]);
                formData.append("lat", $("input#lat").val());
                formData.append("lng", $("input#lng").val());
                formData.append("location", $("input#location").val());
                formData.append("desc", $("input#desc").val());
                formData.append("timeStamp", $("input#timeStamp").val());
                formData.append("userName", $("input#userName").val());
                formData.append("reportTypeId", $("input#reportTypeId").val());

                $.ajax({
                    type: "POST",
                    data: formData,
                    timeout: 10000,
                    url: "../Services/PostReport.ashx",
                    processData: false,
                    contentType: false,
                    success: function (data, textStatus, xhr) {
                        alert(xhr.responseText);
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        alert("error: " + errorThrown);
                    }
                });
            }); 
        })
    </script>
    <title>Test Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Create Report Test</h1>
        <div>
            latitude: <input id="lat" placeholder="latitude" value="40.099876" /> <br />
            longitude: <input id="lng" placeholder="longitude" value="-88.227857" /> <br />
            location: <input id="location" placeholder="location" value="University of Illinois" /> <br />
            description: <input id="desc" placeholder="description" value="test description" /> <br />
            timestamp: <input id="timeStamp" placeholder="timestamp" value="03/26/2014" /> <br />
            username: <input id="userName" placeholder="username" value="chris.ababan" /> <br />
            reportTypeId: <input id="reportTypeId" placeholder="1" value="1" /> <br />

            <h3>Include photo</h3>
            <div id="docsButtons">
                <input id="uploadPicFile" type='file' />
            </div>
        </div>
        <br />
        <input type="button" id="createReport" value="Create Report" />
    </div>
    </form>
</body>
</html>
