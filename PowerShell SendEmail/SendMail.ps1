$EmailFrom = "virmania@GEM.AD"

$EmailTo = "virmania@GEM.AD"

$Subject = "The subject of your email"

$Body = "What do you want your email to say"

$SMTPServer = "172.16.71.27"


$SMTPClient = New-Object Net.Mail.SmtpClient($SmtpServer, 25)

#$SMTPClient.EnableSsl = $true

#$SMTPClient.Credentials = New-Object System.Net.NetworkCredential("user", "password");

$SMTPClient.Send($EmailFrom, $EmailTo, $Subject, $Body)