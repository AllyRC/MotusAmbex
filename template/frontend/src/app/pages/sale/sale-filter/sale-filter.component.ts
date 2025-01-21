import { CommonModule } from '@angular/common';
import { Component, EventEmitter, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DemoNgZorroAntdModule } from '../../../ng-zorro-antd.module';

@Component({
  selector: 'app-sale-filter',
  templateUrl: './sale-filter.component.html',
  styleUrl: './sale-filter.component.css',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    ReactiveFormsModule, DemoNgZorroAntdModule],
  encapsulation: ViewEncapsulation.None
})
export class SaleFilterComponent implements OnInit {
  @Output() filtersData = new EventEmitter<any>();

  form!: FormGroup;
  isCollapsed = false;
  loading = 0;

  constructor(
    private fb: FormBuilder
  ){}

  ngOnInit(): void {
    this.form = this.fb.group({
      saleNumber: [""]
    });
  }

  toggleCollapse() {
    this.isCollapsed = !this.isCollapsed; // Alterna o estado de visibilidade
  }

  clearFilters() {
    this.form.reset();
    this.onSubmit();
  }

  onSubmit(): void {
    this.filtersData.emit(this.form.value);
  }
}
