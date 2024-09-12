import {commonEnvironment} from './environment.common';

const env: Partial<typeof commonEnvironment> = {
  production: true,
  host:""
};

// Export all settings of common replaced by dev options
export const environment = {...commonEnvironment, ...env};
