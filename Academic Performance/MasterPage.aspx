<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterPage.aspx.cs" Inherits="Academic_Performance.HomePage" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-OERcA2EqjJCMA+/3y+gxIOqMEjwtxJY7qPCqsdltbNJuaOe923+mo//f6V8Qbsw3" crossorigin="anonymous"></script>    
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <style>
        .background {
            width: 100%;
            height: 222vh;            
            background-size: 100% 222vh;
            background-repeat: no-repeat;
            background-position: center;
        }

        .logo {
            padding-top: 108px;            
            width: 100px;
            height: 70px;
            background-size: 222px 170px;
            background-repeat: no-repeat;
            background-position: center;
        }

        .title {
            padding-top: 18px;
            margin-left: 108px;
            margin-top: -92px;
        }

        .menu {
            margin-top: -50px;
            float: right;
            margin-left: auto;
        }

        ol {
            display: flex;
            list-style: none;
            text-transform: uppercase;            
        }

        li {
            margin-right: 22px;            
        }

        a {
            text-decoration: none;
            font-family: 'Times New Roman';
            padding: 8px 18px;
            width: 220px;
            height: 50px;
            border: none;
            outline: none;
            color: #000;
            background: #fff;
            cursor: pointer;
            position: relative;
            z-index: 0;
            border-radius: 10px;
        }

        a:before {
            content: '';
            background: linear-gradient(45deg, #ff0000, #ff7300, #fffb00, #48ff00, #00ffd5, #002bff, #7a00ff, #ff00c8, #ff0000);
            position: absolute;
            top: -5px;
            left:-5px;
            background-size: 500%;
            z-index: -1;
            filter: blur(3px);
            width: calc(100% + 10px);
            height: calc(100% + 10px);
            animation: glowing 20s linear infinite;
            opacity: 0;
            transition: opacity .0s ease-in-out;
            border-radius: 14px;
        }

        a:hover:before {
            opacity: 1;    
        }

        a:hover {
            padding: 18px 28px;
            background: #000;    
            color: white;
        }

        a:after {
            z-index: -1;
            content: '';
            position: absolute;
            width: 100%;
            height: 100%;
            left: 0;
            top: 0;
            border-radius: 10px;
        }

        @keyframes glowing {
            0% { background-position: 0 0; }
            50% { background-position: 500% 0; }
            100% { background-position: 0 0; }
        }

        .bodytext {
            margin-top: 70px;
        }       

        .footer {
            margin-top: 20vh;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="Background" class="background" runat="server">            
            <div id="Logo" class="logo" runat="server">

            </div>       
            <div id="Title" runat="server" class="title">

            </div>
            <div id="Menu" runat="server" class="menu">
                
            </div>
            <div id="BodyText" class="bodytext" runat="server">
                
            </div>
            <div id="Footer" runat="server" class="footer">

            </div>
        </div>
    </form>
    <%--<script>
        document.getElementById('fname').setAttribute("value", "Jay Swaminarayan");
    </script>--%>
</body>
</html>