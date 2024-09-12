import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AttachPriceToNamePipe, PromotionDirective } from './share-reference';

@NgModule({
  declarations: [AttachPriceToNamePipe, PromotionDirective],
  imports: [CommonModule],
  exports: [AttachPriceToNamePipe, PromotionDirective],
})
export class ShareModule {}
