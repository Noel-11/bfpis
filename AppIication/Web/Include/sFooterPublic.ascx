<%@ Control Language="VB" AutoEventWireup="false" CodeFile="sFooterPublic.ascx.vb" Inherits="Include_sFooterPublic" %>

 <link href="<%=ResolveClientUrl("~/Scripts/mycss/AppFooter.css")%>" rel="stylesheet" />

<!-- FOOTER -->
<footer class="app-footer">
    <div class="footer-content">

        <div class="footer-logos">
            <img src="<%=ResolveClientUrl("~/Images/ccIcon.png")%>" alt="City College Logo">
            <div class="footer-text">
                <strong>City College Training Online Application</strong>
                <span>Powered by: City Management Information Systems and Innovation Department</span>
            </div>
            <img src="<%=ResolveClientUrl("~/Images/ICTlogo2.png")%>" alt="IT Department Logo">
        </div>

        <div class="footer-actions">
            <a href="https://www.cagayandeoro.gov.ph/" target="_blank" class="footer-btn">Visit Official Website
            </a>
            <a href="https://www.facebook.com/profile.php?id=61564466020931" target="_blank" class="footer-btn outline">Official Facebook Page
            </a>
        </div>
 
        <div class="footer-copy">
            © 2026 City College. All Rights Reserved.
        </div>

    </div>
</footer>



