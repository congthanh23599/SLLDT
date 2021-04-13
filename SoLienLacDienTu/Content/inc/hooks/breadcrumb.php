<?php 

if ( ! function_exists( 'education_x_add_breadcrumb' ) ) :

	/**
	 * Add breadcrumb.
	 *
	 * @since 1.0.0
	 */
	function education_x_add_breadcrumb() {

		// Bail if Breadcrumb disabled.
		$breadcrumb_type = education_x_get_option( 'breadcrumb_type' );
		if ( 'disabled' === $breadcrumb_type ) {
			return;
		}
		// Bail if Home Page.
		if ( is_front_page() || is_home() ) {
			return;
		}
		// Render breadcrumb.
		echo '<div class="col-md-12">';
		switch ( $breadcrumb_type ) {
			case 'simple':
				education_x_simple_breadcrumb();
			break;

			case 'advanced':
				if ( function_exists( 'bcn_display' ) ) {
					bcn_display();
				}
			break;

			default:
			break;
		}
		echo '</div><!-- .container -->';
		return;

	}

endif;

add_action( 'education_x_action_breadcrumb', 'education_x_add_breadcrumb' , 10 );
