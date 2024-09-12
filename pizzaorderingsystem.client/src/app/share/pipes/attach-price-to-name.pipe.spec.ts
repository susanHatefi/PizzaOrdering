import { AttachPriceToNamePipe } from './attach-price-to-name.pipe';

describe('AttachPriceToNamePipe', () => {
  it('create an instance', () => {
    const pipe = new AttachPriceToNamePipe();
    expect(pipe).toBeTruthy();
  });
});
