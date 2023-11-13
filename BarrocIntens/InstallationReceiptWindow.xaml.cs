using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Installation receipt assister please enter the relevant information:");
        Console.Write("Enter employee name: ");
        string employeeName = Console.ReadLine();

        // Get installation details
        Console.Write("Enter coffee machine model: ");
        string machineModel = Console.ReadLine();

        Console.Write("Enter installation date (YYYY-MM-DD): ");
        string installationDate = Console.ReadLine();

        Console.Write("Enter connection costs: ");
        decimal connectionCosts = decimal.Parse(Console.ReadLine());

        // Generate receipt
        string receipt = GenerateReceipt(employeeName, machineModel, installationDate, connectionCosts);

        // Save receipt to a file
        SaveReceiptToFile(receipt);

        Console.WriteLine("Installation receipt generated and saved successfully!");
    }

    static string GenerateReceipt(string employeeName, string machineModel, string installationDate, decimal connectionCosts)
    {
        string receiptTemplate = $"Installation Receipt\n\n" +
                                 $"Employee: {employeeName}\n" +
                                 $"Coffee Machine Model: {machineModel}\n" +
                                 $"Installation Date: {installationDate}\n" +
                                 $"Connection Costs: ${connectionCosts}\n\n" +
                                 "Thank you for choosing our services!";

        return receiptTemplate;
    }

    static void SaveReceiptToFile(string receipt)
    {
        string fileName = "InstallationReceipt.txt";

        try
        {
            File.WriteAllText(fileName, receipt);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving receipt to file: {ex.Message}");
        }
    }
}


