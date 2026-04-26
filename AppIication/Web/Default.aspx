<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false" CodeFile="Default.aspx.vb" Inherits="_Default" Theme="Skins" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html>
<%@ Register Src="~/Include/wucErrorMessageBox.ascx" TagName="wucError" TagPrefix="wucError" %>
<%@ Register Src="~/Include/sFooter.ascx" TagName="sFooter" TagPrefix="uc3" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta name="robots" content="noindex, nofollow" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="Shortcut Icon" href="~/favicon.ico" type="image/x-icon" />
    <title>BFPIS</title>

    <%--<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Scripts/NiceAdmin/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Inter:wght@400;500;600;700&display=swap" rel="stylesheet" />
    <%-- <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css" />--%>
    <link href="Scripts/NiceAdmin/vendor/bootstrap-icons/bootstrap-icons.css" rel="stylesheet" />
    <%--<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>--%>
    <script src="Scripts/NiceAdmin/vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="Scripts/myJS/myJs.js"></script>


    <style>
      
         :root {
            --primary-blue: #0ab1e3;
            --accent-gold: #ffc107;
            --rise-blue: #619fb4;
            --text-dark: #2d3436;
            --text-muted: #636e72;
            --glass-bg: rgba(255, 255, 255, 0.9);
        }

        body {
            font-family: 'Inter', sans-serif;
            min-height: 100vh;
            margin: 0;
            /* Your Requested Gradient */
            background: linear-gradient(to bottom right, #3fe30a, #f5f0f0);
            background-attachment: fixed;
            display: flex;
            flex-direction: column;
        }

        /* --- Main Layout --- */
        .page-wrapper {
            flex: 1;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 3rem 1rem;
        }

        .login-container {
            display: flex;
            background: var(--glass-bg);
            backdrop-filter: blur(15px);
            -webkit-backdrop-filter: blur(15px);
            border-radius: 2rem;
            overflow: hidden;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
            max-width: 1000px;
            width: 100%;
            border: 1px solid rgba(255, 255, 255, 0.4);
        }

        /* --- Side Image Section --- */
        .login-side-image {
            width: 50%;
            background-image: url('Images/login.png');
            background-size: cover;
            background-position: center;
            position: relative;
            min-height: 500px;
        }

        .image-overlay {
            position: absolute;
            bottom: 0;
            left: 0;
            right: 0;
            padding: 3rem;
            background: linear-gradient(transparent, rgba(0,0,0,0.8));
            color: white;
        }

        /* --- Login Form Section --- */
        .login-form-side {
            width: 50%;
            padding: 3.5rem;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }

        .form-control {
            border-radius: 0.75rem;
            padding: 0.75rem 1rem;
            border: 1px solid #dee2e6;
            background-color: #f8f9fa;
        }

        .form-control:focus {
            border-color: var(--primary-blue);
            box-shadow: 0 0 0 0.25rem rgba(10, 177, 227, 0.15);
            background-color: #fff;
        }

        .input-group-text {
            border-radius: 0.75rem 0 0 0.75rem;
            background-color: #f8f9fa;
            border-right: none;
        }

        .password-toggle {
            cursor: pointer;
            border-left: none;
            border-radius: 0 0.75rem 0.75rem 0;
        }

        .btn-login {
            background-color: var(--primary-blue);
            border: none;
            border-radius: 0.75rem;
            padding: 0.9rem;
            font-weight: 700;
            color: white;
            transition: all 0.3s ease;
            letter-spacing: 0.5px;
        }

        .btn-login:hover {
            background-color: #0894be;
            transform: translateY(-2px);
            box-shadow: 0 8px 20px rgba(10, 177, 227, 0.3);
        }

        .visitor-stat {
            font-size: 0.75rem;
            color: var(--text-muted);
            background: rgba(0,0,0,0.04);
            padding: 6px 14px;
            border-radius: 50px;
            border: 1px solid rgba(0,0,0,0.05);
        }

          /* --- Responsive Queries --- */
        @media (max-width: 992px) {
            .login-container { max-width: 800px; }
            .login-form-side { padding: 2.5rem; }
        }

        @media (max-width: 768px) {
            .login-container { flex-direction: column; max-width: 450px; }
            .login-side-image { width: 100%; height: 250px; min-height: 250px; }
            .login-form-side { width: 100%; padding: 2.5rem; }
            .image-overlay { padding: 1.5rem; }
        }
    </style>

    <!-- Google tag (gtag.js) -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=G-BTES5DW7T1"></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'G-BTES5DW7T1');

    </script>

    <script>
        function togglePassword() {
            const passwordInput = document.getElementById('txtPassword');
            const toggleIcon = document.getElementById('toggleIcon');

            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                toggleIcon.classList.replace('bi-eye-slash', 'bi-eye');
            } else {
                passwordInput.type = 'password';
              
                toggleIcon.classList.replace('bi-eye', 'bi-eye-slash');
            }
        }
    </script>

</head>
<body>

    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

         <div class="page-wrapper">
        <div class="login-container">
            <div class="login-side-image">
               <%-- <div class="image-overlay d-none d-md-block">
                    <h2 class="h4 fw-bold">Inclusive Future</h2>
                    <p class="small mb-0 opacity-75">Streamlining accessibility services for the PWD community of Cagayan de Oro.</p>
                </div>--%>
            </div>

            <div class="login-form-side">
                <div class="text-center mb-5">
                    <div class="d-inline-block p-3 rounded-circle bg-info bg-opacity-10 mb-3">
                       <%-- <i class="bi bi-person-wheelchair text-info fs-1"></i>--%>
                        <%--<img src="Images/PWDLogo_NB.png" width="70" />--%>
                    </div>
                    <h1 class="h4 fw-bold mb-1">BARANGAY INFO PROFILE INFORMATION SYSTEM</h1>
                    <p class="text-muted small fw-medium">City Social Welfare and Developemt Department</p>
                </div>

                <div>
                    <div class="mb-3">
                        <label class="form-label small fw-bold text-uppercase" style="font-size: 0.65rem; letter-spacing: 1px;">User ID</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-person text-muted"></i></span>
                             <asp:TextBox runat="server" CssClass="form-control border-start-0" Placeholder="Enter your ID" ID="txtUserId" onkeyup="clickEnterSearch('btnLogin');" />
                            <%--<input type="text" class="form-control border-start-0" placeholder="Enter your ID" required />--%>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label class="form-label small fw-bold text-uppercase" style="font-size: 0.65rem; letter-spacing: 1px;">Password</label>
                        <div class="input-group">
                            <span class="input-group-text"><i class="bi bi-lock text-muted"></i></span>
                             <asp:TextBox runat="server" CssClass="form-control border-start-0 border-end-0" TextMode="Password" Placeholder="Enter Password" ID="txtPassword" onkeyup="clickEnterSearch('btnLogin');" />
                            <%--<input type="password" id="passwordInput" class="form-control border-start-0 border-end-0" placeholder="" required />--%>
                            <span class="input-group-text bg-white password-toggle" onclick="togglePassword()">
                                <i class="bi-eye-slash text-muted" id="toggleIcon"></i>
                            </span>
                        </div>
                    </div>

                    <button runat="server" type="button" class="btn btn-login w-100 mb-4" id="btnLogin">
                        LOGIN TO ACCOUNT
                    </button>

                  <%--  <div class="d-flex justify-content-between small px-1">
                        <a href="#" class="text-muted text-decoration-none hover-link">Forgot Password?</a>
                        <a href="#" class="text-info fw-bold text-decoration-none">Create Account</a>
                    </div>--%>
                </div>

                <div class="mt-5 pt-3 border-top d-flex justify-content-between">
                    <div class="visitor-stat">Today: <strong><label runat="server" id="lblTodayVisitor"></label></strong></div>
                    <div class="visitor-stat">Total: <strong><label runat="server" id="lblTotalVisitor"></label></strong></div>
                </div>
            </div>
        </div>
    </div>
        
        <!-- Footer -->
        <div class="footer">
            <uc3:sFooter ID="sFooter1" runat="server" />
        </div>

    </form>
</body>
</html>

