# SqlInjection
A SQLEncoder library that will sanitize user input such as apostophe and comment remarks to avoid scenarios of sql injection.

Samples include - 
1.MVC application that uses Action filters to loop through the properties and sanitize any string value.
2.Web forms client that uses a custom HttpModule to intercept the request and sanitize any string value.
