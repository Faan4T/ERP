import { ERPTemplatePage } from './app.po';

describe('ERP App', function() {
  let page: ERPTemplatePage;

  beforeEach(() => {
    page = new ERPTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
