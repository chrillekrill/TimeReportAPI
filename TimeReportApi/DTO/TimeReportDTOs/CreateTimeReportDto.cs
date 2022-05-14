﻿namespace TimeReportApi.DTO.TimeReportDTOs;

public class CreateTimeReportDto
{
    public DateTime Date { get; set; }
    public int Minutes { get; set; }
    public string Description { get; set; }
    public string ProjectId { get; set; }
    public string CustomerId { get; set; }
}