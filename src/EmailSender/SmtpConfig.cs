﻿namespace EmailSender;

public class SmtpConfig
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string From { get; set; }
    public string Password { get; set; }
}