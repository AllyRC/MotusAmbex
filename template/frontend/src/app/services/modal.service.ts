import { Injectable } from '@angular/core';
import { NzModalService } from 'ng-zorro-antd/modal';
import { Observable } from 'rxjs';
import { Subject } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class ModalService {
    constructor(private nzModalService: NzModalService) { }

    cancelar(): Observable<string> {
        const resultSubject = new Subject<string>();

        const modal = this.nzModalService.warning({
            nzTitle: 'Cuidado! Tem certeza de que deseja cancelar?',
            nzContent: 'Ao cancelar essa ação todos os dados serão perdidos.',
            nzOkText: 'Confirmar',
            nzOkDanger: true,
            nzCancelText: 'Cancelar',
            nzOnOk: () => resultSubject.next('ok'),
            nzOnCancel: () => resultSubject.next('cancel')
        });

        modal.afterClose.subscribe(result => {
            resultSubject.complete();
        });

        return resultSubject.asObservable();
    }

    deletar(): Observable<string> {
        const resultSubject = new Subject<string>();

        const modal = this.nzModalService.warning({
            nzTitle: 'Atenção!',
            nzContent: 'Você tem certeza que deseja excluir este item?',
            nzOkText: 'Excluir',
            nzOkDanger: true,
            nzCancelText: 'Cancelar',
            nzOnOk: () => resultSubject.next('ok'),
            nzOnCancel: () => resultSubject.next('cancel')
        });

        modal.afterClose.subscribe(result => {
            resultSubject.complete();
        });

        return resultSubject.asObservable();
    }

    abrirWarning(title: string, content: string, okText: string, okDanger: boolean): Observable<string> {
        const resultSubject = new Subject<string>();

        const modal = this.nzModalService.warning({
            nzTitle: title,
            nzContent: content,
            nzOkText: okText,
            nzOkDanger: okDanger,
            nzCancelText: 'Cancelar',
            nzOnOk: () => resultSubject.next('ok'),
            nzOnCancel: () => resultSubject.next('cancel')
        });

        modal.afterClose.subscribe(result => {
            resultSubject.complete();
        });

        return resultSubject.asObservable();
    }
}

