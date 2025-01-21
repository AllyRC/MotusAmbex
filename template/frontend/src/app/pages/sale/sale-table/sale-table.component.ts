import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import NzColumn from '../../../models/column';
import { DemoNgZorroAntdModule } from '../../../ng-zorro-antd.module';
import { NzMessageService } from 'ng-zorro-antd/message';
import { TokenStorageService } from '../../../services/token.storage.service';
import { DataResetService } from '../../../services/data.reset.service';
import { ModalService } from '../../../services/modal.service';
import SaleModel from '../../../models/interfaces/SaleModel';
import { SalesService } from '../../../services/sales.service';

@Component({
  selector: 'app-sale-table',
  templateUrl: './sale-table.component.html',
  styleUrl: './sale-table.component.css',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    DemoNgZorroAntdModule
  ]
})
export class SaleTableComponent implements OnInit {
  isGestor: boolean = false;
  @Input() filter: any;
  currentPage = 1;
  itemsPerPage = 10;
  loading = 0;
  items: any;
  totalData = 0;
  filters: any = {};

  dtResetObservable: any;

  listOfColumns: NzColumn[] = [
    {
      title: 'Sale number',
      field: 'saleNumber',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: null
    },
    {
      title: 'Sale date',
      field: 'saleDate',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: null
    },
    {
      title: 'Customer name',
      field: 'customerName',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: null
    },
    {
      title: 'Total sale amount',
      field: 'totalSaleAmount',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: null
    },
    {
      title: 'Branch',
      field: 'branch',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: null
    },
    {
      title: 'Action',
      field: '',
      sortOrder: null,
      sortFn: null,
      sortDirections: [null],
      nzAlign: 'right'
    },
  ];

  constructor(private dataResetService: DataResetService,
    private salesService: SalesService,
    private modalService: ModalService,
    private message: NzMessageService, private tokenStorageService: TokenStorageService) { }

  ngOnInit(): void {
    this.dtResetObservable = this.dataResetService.resetData.subscribe((res) => {
      this.getData({ pageNumber: this.currentPage, pageSize: this.itemsPerPage });
    });
  }

  ngOnDestroy(): void {
    this.dtResetObservable?.unsubscribe();
  }

  ngOnChanges() {
    if (this.filter) {
      this.filters = this.filter;

      this.getData(this.filters);
    } else {
      this.getData({ pageNumber: this.currentPage, pageSize: this.itemsPerPage });
    }
  }

  getData(data: any) {
    this.loading++;
    this.filters.pageNumber = data.pageNumber;
    this.filters.pageSize = data.pageSize;

    this.items = [];
    this.salesService.getList(this.filters).subscribe((res) => {
      if (res.success === true) {
        this.loading--;
        this.items = res.data;
        this.totalData = res.totalRecords ?? 0
      } else {
        this.loading--;
        this.items = [];
        this.totalData = 0;
      }
    }, (error: any) => {
      this.loading--;
      this.items = [];
      this.totalData = 0
    })
  }

  onPageChange(page: number): void {
    if (this.loading === 0) {
      this.currentPage = page;
      this.filters.skip = (this.currentPage - 1) * this.itemsPerPage;
      this.getData(this.filters);
    }
  }

  onPageSizeChange(size: number): void {
    if (this.loading === 0) {
      this.itemsPerPage = size;
      this.currentPage = 1;
      this.filters.take = this.itemsPerPage;
      this.filters.skip = 0;
      this.getData(this.filters);
    }
  }

  delete(item: SaleModel) {
    this.loading++;
    this.modalService.deletar().subscribe(result => {
      if (this.isGestor === false) {
        if (result === 'ok') {
          this.salesService.deleteSale({ id: item.id }).subscribe((response) => {
            if (response.success) {
              this.loading--;
              this.message.create('success', `O item foi excluido com sucesso!`);
              this.dataResetService.resetData.emit();
            }
          }, (error) => {
            this.loading--;
            this.message.create('error', `Erro ao excluir`);
            console.error('error:', error);
          });
        }
      } else {
        this.loading--;
        this.message.create('error', `Seu nível de acesso não permite essa operação.`);
      }
    });
  }
}
