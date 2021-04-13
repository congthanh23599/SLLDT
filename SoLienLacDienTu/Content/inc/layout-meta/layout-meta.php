<?php
/**
 * Implement theme metabox.
 *
 * @package education-x
 */

if ( ! function_exists( 'education_x_add_theme_meta_box' ) ) :

	/**
	 * Add the Meta Box
	 *
	 * @since 1.0.0
	 */
	function education_x_add_theme_meta_box() {

		$apply_metabox_post_types = array( 'post', 'page' );

		foreach ( $apply_metabox_post_types as $key => $type ) {
			add_meta_box(
				'education-x-theme-settings',
				esc_html__( 'Single Page/Post Settings', 'education-x' ),
				'education_x_render_theme_settings_metabox',
				$type
			);
		}

	}

endif;

add_action( 'add_meta_boxes', 'education_x_add_theme_meta_box' );

if ( ! function_exists( 'education_x_render_theme_settings_metabox' ) ) :

	/**
	 * Render theme settings meta box.
	 *
	 * @since 1.0.0
	 */
	function education_x_render_theme_settings_metabox( $post, $metabox ) {

		$post_id = $post->ID;
		$education_x_post_meta_value = get_post_meta($post_id);

		// Meta box nonce for verification.
		wp_nonce_field( basename( __FILE__ ), 'education_x_meta_box_nonce' );
		// Fetch Options list.
		$page_layout = get_post_meta($post_id,'education-x-meta-select-layout',true);
		$page_image_layout = get_post_meta($post_id,'education-x-meta-image-layout',true);
	?>
	<div id="education-x-settings-metabox-container" class="education-x-settings-metabox-container">
		<div id="education-x-settings-metabox-tab-layout">
			<h4><?php echo __( 'Layout Settings', 'education-x' ); ?></h4>

			<div class="education-x-row-content">
                <!-- Checkbox Field-->
                <p>
                <div class="education-x-row-content">
                    <label for="education-x-meta-checkbox">
                        <input type="checkbox" name="education-x-meta-checkbox" id="education-x-meta-checkbox"
                               value="yes" <?php if (isset($education_x_post_meta_value['education-x-meta-checkbox'])) {
                            checked($education_x_post_meta_value['education-x-meta-checkbox'][0], 'yes');
                        }
                        ?>/>
                        <?php _e('Check To Use Featured Image As Banner Image', 'education-x') ?>
                    </label>
                </div>
                </p>
			     <!-- Select Field-->
			        <p>
			            <label for="education-x-meta-select-layout" class="education-x-row-title">
			                <?php _e( 'Single Page/Post Layout', 'education-x' )?>
			            </label>
			            <select name="education-x-meta-select-layout" id="education-x-meta-select-layout">
				            <option value="left-sidebar" <?php selected('left-sidebar',$page_layout);?>>
				            	<?php _e( 'Primary Sidebar - Content', 'education-x' )?>
				            </option>
				            <option value="right-sidebar" <?php selected('right-sidebar',$page_layout);?>>
				            	<?php _e( 'Content - Primary Sidebar', 'education-x' )?>
				            </option>
				            <option value="no-sidebar" <?php selected('no-sidebar',$page_layout);?>>
				            	<?php _e( 'No Sidebar', 'education-x' )?>
				            </option>
			            </select>
			        </p>

		         <!-- Select Field-->
		            <p>
		                <label for="education-x-meta-image-layout" class="education-x-row-title">
		                    <?php _e( 'Single Page/Post Image Layout', 'education-x' )?>
		                </label>
                        <select name="education-x-meta-image-layout" id="education-x-meta-image-layout">
            	            <option value="full" <?php selected('full',$page_image_layout);?>>
            	            	<?php _e( 'Full', 'education-x' )?>
            	            </option>
            	            <option value="left" <?php selected('left',$page_image_layout);?>>
            	            	<?php _e( 'Left', 'education-x' )?>
            	            </option>
            	            <option value="right" <?php selected('right',$page_image_layout);?>>
            	            	<?php _e( 'Right', 'education-x' )?>
            	            </option>
            	            <option value="no-image" <?php selected('no-image',$page_image_layout);?>>
            	            	<?php _e( 'No Image', 'education-x' )?>
            	            </option>
                        </select>
		            </p>
			</div><!-- .education-x-row-content -->
		</div><!-- #education-x-settings-metabox-tab-layout -->
	</div><!-- #education-x-settings-metabox-container -->

    <?php
	}

endif;



if ( ! function_exists( 'education_x_save_theme_settings_meta' ) ) :

	/**
	 * Save theme settings meta box value.
	 *
	 * @since 1.0.0
	 *
	 * @param int     $post_id Post ID.
	 * @param WP_Post $post Post object.
	 */
	function education_x_save_theme_settings_meta( $post_id, $post ) {

		// Verify nonce.
		if ( ! isset( $_POST['education_x_meta_box_nonce'] ) || ! wp_verify_nonce( $_POST['education_x_meta_box_nonce'], basename( __FILE__ ) ) ) {
			  return; }

		// Bail if auto save or revision.
		if ( defined( 'DOING_AUTOSAVE' ) || is_int( wp_is_post_revision( $post ) ) || is_int( wp_is_post_autosave( $post ) ) ) {
			return;
		}

		// Check the post being saved == the $post_id to prevent triggering this call for other save_post events.
		if ( empty( $_POST['post_ID'] ) || $_POST['post_ID'] != $post_id ) {
			return;
		}

		// Check permission.
		if ( 'page' === $_POST['post_type'] ) {
			if ( ! current_user_can( 'edit_page', $post_id ) ) {
				return; }
		} else if ( ! current_user_can( 'edit_post', $post_id ) ) {
			return;
		}
        $education_x_meta_checkbox = isset($_POST['education-x-meta-checkbox']) ? esc_attr($_POST['education-x-meta-checkbox']) : '';
        update_post_meta($post_id, 'education-x-meta-checkbox', sanitize_text_field($education_x_meta_checkbox));

		$education_x_meta_select_layout =  isset( $_POST[ 'education-x-meta-select-layout' ] ) ? esc_attr($_POST[ 'education-x-meta-select-layout' ]) : '';
		if(!empty($education_x_meta_select_layout)){
			update_post_meta($post_id, 'education-x-meta-select-layout', sanitize_text_field($education_x_meta_select_layout));
		}
		$education_x_meta_image_layout =  isset( $_POST[ 'education-x-meta-image-layout' ] ) ? esc_attr($_POST[ 'education-x-meta-image-layout' ]) : '';
		if(!empty($education_x_meta_image_layout)){
			update_post_meta($post_id, 'education-x-meta-image-layout', sanitize_text_field($education_x_meta_image_layout));
		}
	}

endif;

add_action( 'save_post', 'education_x_save_theme_settings_meta', 10, 2 );