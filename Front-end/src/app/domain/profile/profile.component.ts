import { Component, OnInit, ViewChild } from '@angular/core';
import { AuthService } from '../../core/modules/services/auth.service';
import { Router } from '@angular/router';
import { RecordService } from '../../core/modules/services/record.service';
import { ExecutionResponse } from '../../core/modules/interfaces/execution-response';
import { RecordResponse } from '../../core/modules/interfaces/record-response';
import { DatePipe } from '@angular/common';
import {
  ApexAxisChartSeries,
  ApexTitleSubtitle,
  ApexDataLabels,
  ApexChart,
  ApexPlotOptions,
  ChartComponent,
  ApexXAxis,
  ApexYAxis,
  ApexStroke,
  ApexGrid
} from 'ng-apexcharts';

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  title: ApexTitleSubtitle;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  stroke?: ApexStroke;
  grid?: ApexGrid;
};

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
  providers: [DatePipe]
})
export class ProfileComponent implements OnInit {
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptions: ChartOptions;

  Records: RecordResponse[] = [];
  userDetail: any = null;

  constructor(private authService: AuthService, private recordService: RecordService, private router: Router) {
    this.chartOptions = {
      series: [],
      chart: {
        background: '#1B2631',
        height: 250,
        width: '100%',
        type: 'heatmap',
        toolbar: {
          show: false
        }
      },
      dataLabels: {
        enabled: false
      },
      plotOptions: {
        heatmap: {
          radius: 4,  // Reduced radius for rounded corners
          enableShades: false,
          distributed: true,
          colorScale: {
            ranges: [
              { from: 0, to: 0, color: '#566573', name: 'No Activity' },
              { from: 1, to: 2, color: '#E59866', name: 'Low' },
              { from: 3, to: 5, color: '#E67E22', name: 'Medium' },
              { from: 6, to: 9, color: '#D35400', name: 'High' },
              { from: 10, to: 1000, color: '#6E2C00', name: 'Very High' }
            ]
          },
          useFillColorAsStroke: true
        }
      },
      title: {
        text: 'User Activity Heatmap',
        align: 'center',
        style: {
          fontSize: '18px',
          fontFamily: 'Arial, sans-serif',
          color: '#333333'
        }
      },
      xaxis: {
        type: 'category',
        labels: {
          show: false,
          style: {
            colors: '#666666',
            fontSize: '12px'
          }
        },
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        }
      },
      yaxis: {
        labels: {
          show: true,
          align: 'right',
          offsetX: 10,
          style: {
            colors: '#666666',
            fontSize: '12px'
          }
        },
        reversed: true,
        axisBorder: {
          show: false
        },
        axisTicks: {
          show: false
        }
      },
      stroke: {
        width: 1
      },
      grid: {
        padding: {
          top: 0,
          right: 0,
          bottom: 0,
          left: 0
        }
      }
    };
  }

  ngOnInit(): void {
    this.userDetail = this.authService.getUserDetail();

    this.recordService.read(this.authService.getUserDetail()?.id).subscribe(
      (response: ExecutionResponse) => {
        if (response.success) {
          this.Records = response.result;
          console.log('Records fetched:', this.Records); // Add logging here
          this.updateChartSeries(); // Call function to update series
        } else {
          console.error('Error retrieving records', response.message);
        }
      },
      (error: any) => {
        console.error('Error retrieving records', error);
      }
    );
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/typing-game']);
  }

  // Update chart series based on fetched records
  updateChartSeries(): void {
    const daysOfWeek = ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'];
  
    if (this.Records) {
      this.chartOptions.series = daysOfWeek.map((day, index) => ({
        name: day,
        data: this.generateData(this.Records.filter(record => new Date(record.dateRecord).getDay() === index), index)
      }));
      console.log('Chart series updated:', this.chartOptions.series);
      
      // Update yaxis labels
      this.chartOptions.yaxis = {
        ...this.chartOptions.yaxis,
        labels: {
          formatter: function(value) {
            return daysOfWeek[value];
          }
        }
      };
    } else {
      console.error('Records not defined');
      this.chartOptions.series = [];
    }
  }

  // Generate data for the chart
  generateData(records: RecordResponse[], dayOfWeek: number): { x: string; y: number }[] {
    const startDate = new Date(2024, 0, 1); // January 1, 2024
    const endDate = new Date(2024, 11, 31); // December 31, 2024
    const dateCounts: { [key: string]: number } = {};
  
    // Initialize all dates for this day of the week
    let currentDate = new Date(startDate);
    currentDate.setDate(currentDate.getDate() + (dayOfWeek - currentDate.getDay() + 7) % 7);
    while (currentDate <= endDate) {
      const weekNumber = this.getWeekNumber(currentDate);
      dateCounts[weekNumber] = 0;
      currentDate.setDate(currentDate.getDate() + 7);
    }
  
    // Count records for each week
    records.forEach(record => {
      const recordDate = new Date(record.dateRecord);
      const weekNumber = this.getWeekNumber(recordDate);
      if (dateCounts[weekNumber] !== undefined) {
        dateCounts[weekNumber]++;
      }
    });
  
    // Convert to series data
    const series: { x: string; y: number }[] = Object.entries(dateCounts).map(([week, count]) => ({
      x: week,
      y: count
    }));
  
    return series;
  }
  
  // Helper method to get week number
  getWeekNumber(date: Date): string {
    const d = new Date(Date.UTC(date.getFullYear(), date.getMonth(), date.getDate()));
    const dayNum = d.getUTCDay() || 7;
    d.setUTCDate(d.getUTCDate() + 4 - dayNum);
    const yearStart = new Date(Date.UTC(d.getUTCFullYear(),0,1));
    return Math.ceil((((d.getTime() - yearStart.getTime()) / 86400000) + 1)/7).toString();
  }
}
