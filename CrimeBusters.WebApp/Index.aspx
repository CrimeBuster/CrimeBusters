<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CrimeBusters.WebApp.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="Content/themes/illinoisTheme/jquery-ui-1.10.4.custom.min.css" rel="stylesheet" />
    <link href="Content/index.css" rel="stylesheet" />
    <link href="Content/bootstrap.css" rel="stylesheet" />
    <link href="Content/bootstrap.css.map" rel="stylesheet" />
    <script src="https://maps.googleapis.com/maps/api/js?v=3.exp&sensor=false"></script>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/jquery-ui-1.10.4.custom.min.js"></script>
    <title>Crime Buster</title>
</head>
<body id="dashboard">
    <form id="form1" runat="server">
        <div id="main">
            <div id="CrimeBusterTitle">Crime Buster</div>
            <div id="user" class="dropdown">
              <span class="dropdown-toggle" data-toggle="dropdown" id="userLoginName"></span>               
                 <ul class="dropdown-menu cbMenu">
                    <li><a href="#" id="Hi_Alert">Hi Alert</a></li>
                    <li><a href="#" id="Lo_Alert">Lo Alert</a></li>
                    <li class="divider"></li>
                    <li><a href="#" id="signOut">Sign Out</a></li>
                 </ul>
            </div>
            <div id="map"></div>
        </div>
        <div id="uploadedImageWindow" style="display: none">
            <img alt="uploaded image" height="400" width="300" />
        </div>
    </form>
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/index.js"></script>
    <script src="Scripts/header.js"></script>
</body>
</html>
