# CommentFeedback

Used for user feedback on various sites.

A link is embedded in your site.  Once clicked, the user redirects to the Commnts page.  Enters their info and feedback
and if they would like to be contacted.

This app used Microsoft identity, MVC, and Entity Framework.  I built out the admin portion in order to manage users in a GUI
similar to the old Membership model.

This simple app can be used as a template for to manage users and feedback.



Please remember to comment out the forced http in the Global.asax.cs file.
Also, remember to configure the MailSettings and  DefaultConnection String in the web.config.

here's how to use it:

Set up for external site:

        <script src="Scripts/jquery-1.12.4.js"></script>
        <script src="Scripts/jquery.cookie.js"></script>
          <script type="text/javascript">
        $(document).ready(function () {
            document.cookie = window.location.href;
            $.cookie("previousUrl", window.location.href, { path: "/" });
        });



		The above was placed in the site master.

Then I placed this in default on RTA, it can be  placed on any page.

<p><a href="http://you web site/CommentApp/">Comments</a></p>
