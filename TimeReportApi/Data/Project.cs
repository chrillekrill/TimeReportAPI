﻿@model TimeReportMvc.Models.CustomerModels.CustomerNewModel

@{
    ViewData["Title"] = "New customer";
}
<div class="card">
    <div class="card-header">
        <h1>Create customer</h1>
    </div>
    <div class="card-body">
        <form method="post">
            <div class="form-group">
                <label asp-for="Name">Namn</label>
                <input class="form-control" asp-for="Name" />  
                <span asp-validation-for="Name" class="field-validation-error"></span>
            </div>
            
            <button type="submit" class="btn">Save</button>

        </form>

    </div>
</div>