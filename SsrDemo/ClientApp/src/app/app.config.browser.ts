import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { appConfig } from './app.config';
import { APP_BASE_HREF } from '@angular/common';

const getBaseUrl = () => {
  return document.getElementsByTagName('base')[0].href.slice(0, -1);
}

const browserConfig: ApplicationConfig = {
  providers: [
    { provide: APP_BASE_HREF, useFactory: getBaseUrl },
    { provide: 'MESSAGE', useValue: 'Message from browser' }
  ]
};

export const config = mergeApplicationConfig(appConfig, browserConfig);
