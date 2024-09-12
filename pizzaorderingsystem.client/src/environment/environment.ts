import { commonEnvironment } from './environment.common';

const env: Partial<typeof commonEnvironment> = {
  host: 'https://localhost:7114',
};

// Export all settings of common replaced by dev options
export const environment = { ...commonEnvironment, ...env };
