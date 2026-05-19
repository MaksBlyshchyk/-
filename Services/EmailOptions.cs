namespace HRReserveSystem.Services;

public class EmailOptions
{
    public bool Enabled { get; set; }

    public string Host { get; set; } = string.Empty;

    public int Port { get; set; } = 587;

    public bool UseSsl { get; set; } = true;

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string FromEmail { get; set; } = "no-reply@hrreserve.local";

    public string FromName { get; set; } = "HR Reserve System";

    public string OutboxPath { get; set; } = "EmailOutbox";
}
