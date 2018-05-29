# Ideamart IdeaPro API Wrapper For C#
##### https://ideamart.io
This is a wrapper / class library ideamart ideapro APIs. With Ideamart APIs you can send/receive SMS, get SMS delivery notification, invoke a USSD menu with the USSD API, Implement location based services using LBS API or even make an in-app purchse through the carrier billing. IdeaPro is where you can bring your ideas to life and monetize them. 

### Available APIs
- [x] SMS API
- [x] USSD API
- [x] Subscription API
- [ ] LBS (Location based service)
- [ ] CaaS (Charging as a service)

### SMS API (Short Message Service)
This is used to send and receive SMS using a HTTP based API. It sends SMS on behalf of the user, check the delivery status of a sent SMS, receive SMSs with a short code etc. 
You can use this to send a SMS from an application to a mobile phone.

### USSD API (Unstructured Supplementary Service Data)
This allows applications to initiate USSD sessions using a HTTP-based API. You can build USSD menu driven applications, monetize based on menu request, maintain an active session with customers etc.

### Subscription API
Subscription REST API allows a subscriber to register or unregister to or from an app, send subscription notification, query subscription status and query subscriber base size.

### LBS API (Location Based Service)
This allows to send and receive location requests using a REST based API to find the location of users and create rich & real-time applications. 

### CaaS API (Charging as a Service)
This monetizes your app with micro-payments. You can retrieve the account balance and other related information of a given subscriber, charge specific amount from a subscriber’s account etc.

### Install
Available as a NuGet package. You can install it using the NuGet Package Console windows;
```
PM> Install-Package Ideamart -Version 1.3.0
```

## Usage
#### SMS API
```c#
SmsAPI smsAPI = new SmsAPI("APP_ID", "App_Password")
            {
                IsInProduction = false
            };
```
Change `IsInProduction` property value to `true` if you  are using ideamart emulator. this must be `false` in production environment.

##### Send an SMS
Note: User must have subscribed to an application to receive messages. 
```c#
//text message to be sent
string msg = "Hello from ideamart api";
//mobile hash of the user
List<string> numbers = new List<string>()
{
    "Mobile hash"
};
var result = await smsAPI.SendSmsAsync(msg, number);
```
`SendSmsAsync` is an asynchronous task which returns a instance of `SmsStatusResponse` object. 

#### USSD API
##### Invoke a USSD menu
When a user dials USSD short code (#775*xxx#), Ideamart platform sends a HTTP POST request to your application (to the `Connection URL` which you given when provisioning). It'll be `mo-init`, `mo-cont` or `mo-fin` USSD request. based on which USSD operation request user has made, your application should respond. Please refer [Ideamart USSD documentation](http://www.ideamart.lk/web/idea-pro/documentation/idea-pro/ussd-api/) for more details.
```c#
UssdAPI ussdAPI = new UssdAPI("APP_ID", "App_Password")
{
    IsInProduction = true
};
```
Create a USSD menu on user mobile
```c#
string msg = "Welcome to my app!\r\n1. Action one\r\n2. Action 2\r\n3. Action three";
UssdStatusReponse statusReponse = await ussdAPI.SendRequestAsync(msg, "SessionID", UssdAPI.UssdOperation.mt_cont, "MobileHash");
```

#### Subscription API
##### User Subsctiption with Subscription API
```c#
var subscriptionApi = new SubscriptionAPI("APP_ID", "App_Password")
{
    IsInProduction = true
};
```

##### Send user subscription request
```c#
SubscriptionResponse subscriptionResponse = await subscriptionApi.SendSubscriptionRequestAsync("Mobile_Hash", SubscriptionAPI.SubscriptionAction.Subscribe);
```

##### Send unsubscription request
```c#
SubscriptionResponse subscriptionResponse = await subscriptionApi.SendSubscriptionRequestAsync("Mobile_Hash", SubscriptionAPI.SubscriptionAction.Unsubscribe);
```
Both subscription and unsubscription requests return an instance of `SubscriptionResponse`

##### Query Subscribers base
```c#
//make the request
SubscriptionStatusResponse subscriptionStatus = await subscriptionApi.RequestSubscriptionStatusAsync("Mobile_Hash");
//get the status of current subscription
string status = subscriptionStatus.subscriptionStatus;
```

## Questions? Problems?
Open-source projects develop more smoothly when discussions are public.
If you have issues or if you discovered a bug, please report it to the [Project's GitHub Issues](https://github.com/nanosoftlk/Ideamart/issues)

## License
This project is under the [MIT license](http://www.opensource.org/licenses/MIT).
