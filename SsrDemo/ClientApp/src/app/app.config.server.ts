import { mergeApplicationConfig, ApplicationConfig } from '@angular/core';
import { provideServerRendering } from '@angular/platform-server';
import { appConfig } from './app.config';

const serverConfig: ApplicationConfig = {
  providers: [
    provideServerRendering(),
    { provide: 'MESSAGE', useValue: 'Message from server' }
  ]
};

export const config = mergeApplicationConfig(appConfig, serverConfig);
