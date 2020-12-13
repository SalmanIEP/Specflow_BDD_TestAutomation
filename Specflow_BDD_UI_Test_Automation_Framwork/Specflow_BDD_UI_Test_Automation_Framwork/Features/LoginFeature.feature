Feature: LoginFeature
	In Order to login to the okta account user must pass 2FA Authntication 
@login
Scenario Outline:1 Login With Valid Credintials using Okta Verify Factor
Given User must need to login before continue
When User provide Valid "<Email>" and "<Password>"
And Select the Login Button
Then User need to provide Valid Passcode for 2FA to further allowed to move to the site
Then Select Verfiy button
Then User must be Logged in to Sample Site Successfully

Examples: 
| Email                                 | Password    |
| your okta email address               | ********* |


